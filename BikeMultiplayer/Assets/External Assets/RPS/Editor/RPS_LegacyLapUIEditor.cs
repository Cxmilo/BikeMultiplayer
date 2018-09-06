/*
Race Positioning System by Solution Studios

Script Name: RPS_LegacyLapUIEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_LegacyLapUI))]
public class RPS_LegacyLapUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RPS_LegacyLapUI myTarget = (RPS_LegacyLapUI)target;
        GUILayout.Label("Legacy Lap UI Text Element:");
        GUILayout.Label("");
        GUILayout.Label("GUIText:");
        myTarget.textObj = (GUIText)EditorGUILayout.ObjectField(myTarget.textObj, typeof(GUIText), true);
        GUILayout.Label("");
        GUILayout.Label("Show the total number of laps?");
        GUILayout.Label("e.g. show '1/3' instead of just '1'");
        myTarget.showTotalLaps = EditorGUILayout.Toggle(myTarget.showTotalLaps);
        GUILayout.Label("");
        GUILayout.Label("Change text when Finished Race?");
        myTarget.changeWhenFinished = EditorGUILayout.Toggle(myTarget.changeWhenFinished);
        if (myTarget.changeWhenFinished == true)
        {
            myTarget.changeToText = EditorGUILayout.TextField("Change to:", myTarget.changeToText);
            myTarget.changeFontSize = EditorGUILayout.Toggle("Change Font Size?", myTarget.changeFontSize);
            if (myTarget.changeFontSize == true)
            {
                myTarget.newFontSize = EditorGUILayout.IntField("Font Size:", myTarget.newFontSize);
            }
        }
    }
}
