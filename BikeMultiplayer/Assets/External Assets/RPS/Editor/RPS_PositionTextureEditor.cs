/*
Race Positioning System by Solution Studios

Script Name: RPS_PositionTextureEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_PositionTexture))]
public class RPS_PositionTextureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RPS_PositionTexture myTarget = (RPS_PositionTexture)target;
        GUILayout.Label("Position Texture Script:");
        GUILayout.Label("");
        GUILayout.Label("Renderer:");
        myTarget.planeRenderer = (Renderer)EditorGUILayout.ObjectField(myTarget.planeRenderer, typeof(Renderer), true);
        GUILayout.Label("");
        myTarget.textureElementsExpand = EditorGUILayout.Foldout(myTarget.textureElementsExpand, "Race Textures");
        if (myTarget.textureElementsExpand == true)
        {
            int y = 0;
            myTarget.textureElementsSize = EditorGUILayout.IntField("Size", myTarget.textureElementsSize);
            if (myTarget.raceTextures != null)
            {
                if (myTarget.raceTextures.Count != myTarget.textureElementsSize)
                {
                    Texture[] fnewArrayy = new Texture[myTarget.textureElementsSize];
                    for (y = 0; y < myTarget.textureElementsSize; y++)
                    {
                        if (myTarget.raceTextures.Count > y)
                        {
                            fnewArrayy[y] = myTarget.raceTextures[y];
                        }
                    }
                    myTarget.raceTextures = fnewArrayy.ToList();
                }
            }
            if (myTarget.raceTextures == null)
            {
                myTarget.raceTextures = new List<Texture>();
            }
            else
            {
                if (myTarget.textureElementsSize > 0)
                {
                    for (y = 0; y < myTarget.textureElementsSize; y++)
                    {
                        myTarget.raceTextures[y] = (Texture)EditorGUILayout.ObjectField("Position " + (y + 1), myTarget.raceTextures[y], typeof(Texture), true);
                    }
                }
            }
        }
    }
}