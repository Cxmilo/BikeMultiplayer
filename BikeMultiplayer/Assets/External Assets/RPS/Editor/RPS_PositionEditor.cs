/*
Race Positioning System by Solution Studios

Script Name: RPS_PositionEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_Position))]
public class RPS_PositionEditor : Editor
{

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Positioning Script:");
        GUILayout.Label("");
        GUILayout.Label("This object will take part in the race.");
        GUILayout.Label("This script will calculate the current race");
        GUILayout.Label("position of this object.");
        GUILayout.Label("To add a lap system or any UI to this object");
        GUILayout.Label("use the RPS_Inspector script.");
    }
}
