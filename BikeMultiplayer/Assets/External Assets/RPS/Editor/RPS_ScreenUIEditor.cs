/*
Race Positioning System by Solution Studios

Script Name: RPS_ScreenUIEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(RPS_ScreenUI))]
public class RPS_ScreenUIEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		RPS_ScreenUI myTarget = (RPS_ScreenUI)target;
		GUILayout.Label("Screen UI Element:");
		GUILayout.Label ("");
		myTarget.index = (type)EditorGUILayout.EnumPopup("Element Type:", myTarget.index);
		if (myTarget.index == type.Image) {
			myTarget.positionImage = (UnityEngine.UI.Image)EditorGUILayout.ObjectField("UI Image:",myTarget.positionImage, typeof(UnityEngine.UI.Image), true);
			myTarget.spriteElementsExpand = EditorGUILayout.Foldout(myTarget.spriteElementsExpand, "Race Sprites");
			if (myTarget.spriteElementsExpand == true) {
				int y = 0;    
				myTarget.spriteElementsSize = EditorGUILayout.IntField("Size", myTarget.spriteElementsSize);
                if (myTarget.raceSprites != null)
                {
                    if (myTarget.raceSprites.Count != myTarget.spriteElementsSize)
                    {
                        Sprite[] newArrayy = new Sprite[myTarget.spriteElementsSize];
                        for (y = 0; y < myTarget.spriteElementsSize; y++)
                        {
                            if (myTarget.raceSprites.Count > y)
                            {
                                newArrayy[y] = myTarget.raceSprites[y];
                            }
                        }
                        myTarget.raceSprites = newArrayy.ToList();
                    }
                }
                if (myTarget.raceSprites == null)
                {
                    myTarget.raceSprites = new List<Sprite>();
                }
                else
                {
                    if (myTarget.spriteElementsSize > 0)
                    {
                        for (y = 0; y < myTarget.raceSprites.Count; y++)
                        {
                            myTarget.raceSprites[y] = (Sprite)EditorGUILayout.ObjectField("Position " + (y + 1), myTarget.raceSprites[y], typeof(Sprite), true);
                        }
                    }
                }
			}
		} else {
			myTarget.textUI = (UnityEngine.UI.Text)EditorGUILayout.ObjectField("UI Text:",myTarget.textUI, typeof(UnityEngine.UI.Text), true);
			GUILayout.Label ("Use Ending on text? e.g. 'st', 'nd', 'rd'...");
			myTarget.textEndingEnabled = EditorGUILayout.Toggle(myTarget.textEndingEnabled);
		}
	}
}