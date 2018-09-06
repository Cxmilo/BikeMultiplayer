/*
Race Positioning System by Solution Studios

Script Name: RPS_CheckpointsEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_Checkpoints))]
public class RPS_CheckpointsEditor : Editor
{

	int part = 0;
	RPS_Checkpoints tempChecksScript;
	bool infoExpanded = false;

	public override void OnInspectorGUI()
	{
		RPS_Checkpoints myTarget = (RPS_Checkpoints)target;
		GUILayout.Label("Checkpoints Script:");
		GUILayout.Label("");
		if (part == 0) {
			GUILayout.Label("Checkpoints:");
			GUILayout.BeginVertical(GUI.skin.box);
			GUILayout.BeginHorizontal();
			GUILayout.Label("Num:");
			GUILayout.Label("PS Limit Num:");
			GUILayout.Label("Passed on Start:");
			GUILayout.Label("Del:");
			GUILayout.EndHorizontal();
			if (myTarget.checkpoints.Count > 0) {
				int checknum = 0;
				foreach (RPS_Checkpoints.checkpoint check in myTarget.checkpoints) {
					GUILayout.BeginHorizontal();
					GUILayout.Label("" + checknum + ":");
					check.limitNumber = EditorGUILayout.FloatField(check.limitNumber);
					check.passedOnStartLap = EditorGUILayout.Toggle(check.passedOnStartLap);
					if (GUILayout.Button("X")) {
						myTarget.checkpoints.RemoveAt(checknum);
						return;
					}
					GUILayout.EndHorizontal();
					checknum = checknum + 1;
				}
			} else {
				GUILayout.Label("None");
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Add")) {
				RPS_Checkpoints.checkpoint check = new RPS_Checkpoints.checkpoint();
				check.limitNumber = 0.0f;
				check.passedOnStartLap = false;
				myTarget.checkpoints.Add(check);
			}
			GUILayout.FlexibleSpace();
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			GUILayout.Label("");
			infoExpanded = EditorGUILayout.Foldout(infoExpanded, "Help");
			if (infoExpanded == true) {
				GUILayout.Label("PS Limit Num - This is the position number found");
				GUILayout.Label("on the nearest PositionSensor to the checkpoint.");
				GUILayout.Label("You will have to find these manually by clicking");
				GUILayout.Label("on the nearest PositionSensors and looking up the");
				GUILayout.Label("number in the Inspector.");
				GUILayout.Label("");
				GUILayout.Label("Passed On Start - This is whether or not the");
				GUILayout.Label("checkpoint has been passed when the race begins.");
				GUILayout.Label("E.g. If the race starts just before the start line,");
				GUILayout.Label("and after the final checkpoint, then all checkpoints");
				GUILayout.Label("don't need to be passed on that lap so should be");
				GUILayout.Label("ticked.");
				GUILayout.Label("");
				GUILayout.Label("All checkpoints should be in the order of going");
				GUILayout.Label("around the track.");
				GUILayout.Label("The last checkpoint should be just before the");
				GUILayout.Label("start/finish line.");
				GUILayout.Label("The function 'CheckpointPassed' must be called on");
				GUILayout.Label("the appropriate RPS_Checkpoints script when the");
				GUILayout.Label("position sensing object passes a checkpoint with");
				GUILayout.Label("the checkpoint number.");
				GUILayout.Label("");
				GUILayout.Label("Look at the pdf for more information and a step");
				GUILayout.Label("by step guide.");
			}
			GUILayout.Label("");
			if (GUILayout.Button("Paste values from another script")) {
				part = 1;
			}
		}
		if (part == 1) {
			GUILayout.Label("Other RPS_Checkpoints script:");
			tempChecksScript = (RPS_Checkpoints)EditorGUILayout.ObjectField(tempChecksScript, typeof(RPS_Checkpoints), true);
			GUILayout.Label("");
			if (GUILayout.Button("Paste Values")) {
				myTarget.checkpoints = tempChecksScript.checkpoints;
				part = 0;
			}
			GUILayout.Label("");
			if (GUILayout.Button("Back")) {
				part = 0;
			}
		}
	}
}