/*
Race Positioning System by Solution Studios

Script Name: RPS_LapEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_Lap))]
public class RPS_LapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RPS_Lap myTarget = (RPS_Lap)target;
        GUILayout.Label("Lap Script:");
        GUILayout.Label("");
        myTarget.hasEnd = EditorGUILayout.Toggle("Race has End?", myTarget.hasEnd);
        if (myTarget.hasEnd == true)
        {
            myTarget.startLapNumber = EditorGUILayout.IntField("First Lap Number:", myTarget.startLapNumber);
            myTarget.lastLap = EditorGUILayout.IntField("Last Lap Number:", myTarget.lastLap);
            GUILayout.Label("The following toggle will freeze the race position");
            GUILayout.Label("shown when the race has ended, always showing the");
            GUILayout.Label("finished position.");
            myTarget.freezeShownPosAtEnd = EditorGUILayout.Toggle("Freeze Position?", myTarget.freezeShownPosAtEnd);
        }
    }
}
