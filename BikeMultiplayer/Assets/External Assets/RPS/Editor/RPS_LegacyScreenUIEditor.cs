/*
Race Positioning System by Solution Studios

Script Name: RPS_LegacyScreenUIEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_LegacyScreenUI))]
public class RPS_LegacyScreenUIEditor : Editor {

    public override void OnInspectorGUI()
    {
        RPS_LegacyScreenUI myTarget = (RPS_LegacyScreenUI)target;
        GUILayout.Label("Legacy Screen UI Element:");
        GUILayout.Label("");
        myTarget.index = (type)EditorGUILayout.EnumPopup("Element Type:", myTarget.index);
        if(myTarget.index == type.Image) {
            myTarget.positionImage = (GUITexture)EditorGUILayout.ObjectField("GUITexture:", myTarget.positionImage, typeof(GUITexture), true);
            myTarget.texturesElementsExpand = EditorGUILayout.Foldout(myTarget.texturesElementsExpand, "Race Textures");
            if (myTarget.texturesElementsExpand == true)
            {
                int y = 0;
                myTarget.texturesElementsSize = EditorGUILayout.IntField("Size", myTarget.texturesElementsSize);
                if (myTarget.raceTextures != null)
                {
                    if (myTarget.raceTextures.Count != myTarget.texturesElementsSize)
                    {
                        Texture[] newArrayy = new Texture[myTarget.texturesElementsSize];
                        for (y = 0; y < myTarget.texturesElementsSize; y++)
                        {
                            if (myTarget.raceTextures.Count > y)
                            {
                                newArrayy[y] = myTarget.raceTextures[y];
                            }
                        }
                        myTarget.raceTextures = newArrayy.ToList();
                    }
                }
                if (myTarget.raceTextures == null)
                {
                    myTarget.raceTextures = new List<Texture>();
                }
                else
                {
                    if (myTarget.texturesElementsSize > 0)
                    {
                        for (y = 0; y < myTarget.raceTextures.Count; y++)
                        {
                            myTarget.raceTextures[y] = (Texture)EditorGUILayout.ObjectField("Position " + (y + 1), myTarget.raceTextures[y], typeof(Texture), true);
                        }
                    }
                }
            }
        } else {
            myTarget.textUI = (GUIText)EditorGUILayout.ObjectField("GUIText:", myTarget.textUI, typeof(GUIText), true);
            GUILayout.Label("Use Ending on text? e.g. 'st', 'nd', 'rd'...");
            myTarget.textEndingEnabled = EditorGUILayout.Toggle(myTarget.textEndingEnabled);
        }
    }
}
