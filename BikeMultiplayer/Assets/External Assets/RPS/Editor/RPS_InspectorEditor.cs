/*
Race Positioning System by Solution Studios

Script Name: RPS_InspectorEditor.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(RPS_Inspector))]
public class RPS_InspectorEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		RPS_Inspector myTarget = (RPS_Inspector)target;
		if (myTarget.part == 0) {
			if (myTarget.storageObj == null) {
				myTarget.storageObj = GameObject.Find ("RPS_Storage");
				if (myTarget.storageObj == null) {
					myTarget.part = 2;
				}
			}
			if (myTarget.storageObj != null) {
				if (myTarget.storageScript == null) {
					myTarget.storageScript = myTarget.storageObj.gameObject.GetComponent<RPS_Storage>();
					if (myTarget.storageScript == null) {
						myTarget.part = 3;
					}
				}
			}
			if (myTarget.posScript == null) {
				myTarget.posScript = myTarget.gameObject.GetComponent<RPS_Position> ();
			}
			if (myTarget.posScript == null) {
				myTarget.part = 1;
			}
			if (myTarget.part == 0) {
				if (myTarget.insidePart == 0) {
					GUILayout.Label("Add Scripts to show Race Position:");
					if (GUILayout.Button ("Screen UI Texture")) {
						myTarget.insidePart = 1;
					}
					if (GUILayout.Button ("Screen UI Text")) {
						myTarget.insidePart = 2;
					}
					if (GUILayout.Button ("Screen UI Texture (Legacy)")) {
						myTarget.insidePart = 3;
					}
					if (GUILayout.Button ("Screen UI Text (Legacy)")) {
						myTarget.insidePart = 4;
					}
					if (GUILayout.Button("Position on Renderer's Main Texture")) {
						myTarget.insidePart = 5;
					}
					RPS_Checkpoints checkpointscript = myTarget.gameObject.GetComponent<RPS_Checkpoints>();
					if (checkpointscript == null) {
						GUILayout.Label("");
						GUILayout.Label("Add Checkpoints to this object:");
						if (GUILayout.Button("Add Checkpoint System")) {
							myTarget.insidePart = 87;
						}
					}
					RPS_Lap lapSystem = myTarget.gameObject.GetComponent<RPS_Lap> ();
					if (lapSystem == null) {
						GUILayout.Label("");
						GUILayout.Label ("There is no Lap script on this object.");
						GUILayout.Label ("If you want to use laps you need to add");
						GUILayout.Label ("a RPS_Lap script. Click bellow to do this");
						GUILayout.Label ("If you do not want laps, you do not need a");
						GUILayout.Label ("RPS_Lap script.");
						if (GUILayout.Button ("Add Lap Script")) {
							myTarget.insidePart = 6;
						}
					} else {
						GUILayout.Label("");
						GUILayout.Label ("Add Lap UI Elements:");
						if (GUILayout.Button ("Add Lap UI Text")) {
							myTarget.insidePart = 7;
						}
						if (GUILayout.Button ("Add Lap UI Text (Legacy)")) {
							myTarget.insidePart = 8;
						}
					}
				}
				if (myTarget.insidePart == 1) {
					GUILayout.Label("Add Screen UI Texture:");
					GUILayout.Label ("");
					GUILayout.Label ("UI Image:");
					myTarget.screenUIImage = (UnityEngine.UI.Image)EditorGUILayout.ObjectField(myTarget.screenUIImage, typeof(UnityEngine.UI.Image), true);
					GUILayout.Label ("");
					myTarget.spriteElementsExpand = EditorGUILayout.Foldout(myTarget.spriteElementsExpand, "Race Sprites");
					if (myTarget.spriteElementsExpand == true) {
						int y = 0;    
						myTarget.spriteElementsSize = EditorGUILayout.IntField("Size", myTarget.spriteElementsSize);
                        if (myTarget.screenUISprites != null)
                        {
                            if (myTarget.screenUISprites.Length != myTarget.spriteElementsSize)
                            {
                                Sprite[] newArrayy = new Sprite[myTarget.spriteElementsSize];
                                for (y = 0; y < myTarget.spriteElementsSize; y++)
                                {
                                    if (myTarget.screenUISprites.Length > y)
                                    {
                                        newArrayy[y] = myTarget.screenUISprites[y];
                                    }
                                }
                                myTarget.screenUISprites = newArrayy;
                            }
                        }
                        if (myTarget.screenUISprites == null)
                        {
                            myTarget.screenUISprites = new Sprite[0];
                        }
                        else
                        {
                            if (myTarget.spriteElementsSize > 0)
                            {
                                for (y = 0; y < myTarget.spriteElementsSize; y++)
                                {
                                    myTarget.screenUISprites[y] = (Sprite)EditorGUILayout.ObjectField("Position " + (y + 1), myTarget.screenUISprites[y], typeof(Sprite), true);
                                }
                            }
                        }
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_ScreenUI newScript = myTarget.gameObject.AddComponent<RPS_ScreenUI>();
						newScript.index = type.Image;
						newScript.positionImage = myTarget.screenUIImage;
                        newScript.spriteElementsSize = myTarget.spriteElementsSize;
						newScript.raceSprites = myTarget.screenUISprites.ToList();
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 2) {
					GUILayout.Label("Add Screen UI Text:");
					GUILayout.Label ("");
					GUILayout.Label ("UI Text:");
					myTarget.screenUIText = (UnityEngine.UI.Text)EditorGUILayout.ObjectField(myTarget.screenUIText, typeof(UnityEngine.UI.Text), true);
					GUILayout.Label ("Show endings on text strings?");
					GUILayout.Label ("e.g. add the 'st' onto '1st' and 'nd' onto '2nd'");
					myTarget.screenUITextShowEndings = EditorGUILayout.Toggle (myTarget.screenUITextShowEndings);
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_ScreenUI newScriptTwo = myTarget.gameObject.AddComponent<RPS_ScreenUI>();
						newScriptTwo.index = type.Text;
						newScriptTwo.textUI = myTarget.screenUIText;
						newScriptTwo.textEndingEnabled = myTarget.screenUITextShowEndings;
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 3) {
					GUILayout.Label("Add Screen UI Texture (Legacy):");
					GUILayout.Label ("");
					GUILayout.Label("GUITexture:");
					myTarget.legacyScreenUITexture = (GUITexture)EditorGUILayout.ObjectField(myTarget.legacyScreenUITexture, typeof(GUITexture), true);
					GUILayout.Label ("");
					myTarget.legacyScreenUIElementsExpand = EditorGUILayout.Foldout(myTarget.legacyScreenUIElementsExpand, "Race Textures");
					if (myTarget.legacyScreenUIElementsExpand == true) {
						int y = 0;    
						myTarget.legacyScreenUIElementsSize = EditorGUILayout.IntField("Size", myTarget.legacyScreenUIElementsSize);
                        if (myTarget.legacyScreenUIRaceTextures != null)
                        {
                            if (myTarget.legacyScreenUIRaceTextures.Length != myTarget.legacyScreenUIElementsSize)
                            {
                                Texture[] anotherNewArray = new Texture[myTarget.legacyScreenUIElementsSize];
                                for (y = 0; y < myTarget.legacyScreenUIElementsSize; y++)
                                {
                                    if (myTarget.legacyScreenUIRaceTextures.Length > y)
                                    {
                                        anotherNewArray[y] = myTarget.legacyScreenUIRaceTextures[y];
                                    }
                                }
                                myTarget.legacyScreenUIRaceTextures = anotherNewArray;
                            }
                        }
                        if (myTarget.legacyScreenUIRaceTextures == null)
                        {
                            myTarget.legacyScreenUIRaceTextures = new Texture[0];
                        }
                        else
                        {
                            if (myTarget.legacyScreenUIElementsSize > 0)
                            {
                                for (y = 0; y < myTarget.legacyScreenUIElementsSize; y++)
                                {
                                    myTarget.legacyScreenUIRaceTextures[y] = (Texture)EditorGUILayout.ObjectField("Position " + (y + 1), myTarget.legacyScreenUIRaceTextures[y], typeof(Texture), true);
                                }
                            }
                        }
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_LegacyScreenUI newScripttt = myTarget.gameObject.AddComponent<RPS_LegacyScreenUI>();
						newScripttt.index = type.Image;
                        newScripttt.texturesElementsSize = myTarget.legacyScreenUIElementsSize;
						newScripttt.positionImage = myTarget.legacyScreenUITexture;
						newScripttt.raceTextures = myTarget.legacyScreenUIRaceTextures.ToList();
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 4) {
					GUILayout.Label("Add Screen UI Text (Legacy):");
					GUILayout.Label ("");
					GUILayout.Label ("GUIText:");
					myTarget.legacyScreenUIText = (GUIText)EditorGUILayout.ObjectField(myTarget.legacyScreenUIText, typeof(GUIText), true);
					GUILayout.Label ("Show endings on text strings?");
					GUILayout.Label ("e.g. add the 'st' onto '1st' and 'nd' onto '2nd'");
					myTarget.legacyScreenUITextShowEnding = EditorGUILayout.Toggle (myTarget.legacyScreenUITextShowEnding);
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_LegacyScreenUI neewScripttt = myTarget.gameObject.AddComponent<RPS_LegacyScreenUI>();
						neewScripttt.index = type.Text;
						neewScripttt.textUI = myTarget.legacyScreenUIText;
						neewScripttt.textEndingEnabled = myTarget.legacyScreenUITextShowEnding;
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 5) {
					GUILayout.Label("Add Position on Renderer's Main Texture:");
					GUILayout.Label ("");
					GUILayout.Label ("Renderer:");
					myTarget.posTexturePlaneRenderer = (Renderer)EditorGUILayout.ObjectField(myTarget.posTexturePlaneRenderer, typeof(Renderer), true);
					GUILayout.Label ("");
					myTarget.posTextureElementsExpand = EditorGUILayout.Foldout(myTarget.posTextureElementsExpand, "Race Textures");
					if (myTarget.posTextureElementsExpand == true) {
						int y = 0;    
						myTarget.posTextureElementsSize = EditorGUILayout.IntField("Size", myTarget.posTextureElementsSize);
                        if (myTarget.posTextureRaceTextures != null)
                        {
                            if (myTarget.posTextureRaceTextures.Length != myTarget.posTextureElementsSize)
                            {
                                Texture[] fnewArrayy = new Texture[myTarget.posTextureElementsSize];
                                for (y = 0; y < myTarget.posTextureElementsSize; y++)
                                {
                                    if (myTarget.posTextureRaceTextures.Length > y)
                                    {
                                        fnewArrayy[y] = myTarget.posTextureRaceTextures[y];
                                    }
                                }
                                myTarget.posTextureRaceTextures = fnewArrayy;
                            }
                        }
                        if (myTarget.posTextureRaceTextures == null)
                        {
                            myTarget.posTextureRaceTextures = new Texture[0];
                        }
                        else
                        {
                            if (myTarget.posTextureElementsSize > 0)
                            {
                                for (y = 0; y < myTarget.posTextureElementsSize; y++)
                                {
                                    myTarget.posTextureRaceTextures[y] = (Texture)EditorGUILayout.ObjectField("Position " + (y + 1), myTarget.posTextureRaceTextures[y], typeof(Texture), true);
                                }
                            }
                        }
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_PositionTexture newTextureScript = myTarget.gameObject.AddComponent<RPS_PositionTexture>();
						newTextureScript.planeRenderer = myTarget.posTexturePlaneRenderer;
                        newTextureScript.textureElementsSize = myTarget.posTextureElementsSize;
						newTextureScript.raceTextures = myTarget.posTextureRaceTextures.ToList();
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 87) {
					GUILayout.Label("Add Checkpoint System:");
					GUILayout.Label ("");
					GUILayout.Label("Checkpoints need to be added through the");
					GUILayout.Label("RPS_Checkpoints script's Inspector once added");
					GUILayout.Label("");
					GUILayout.Label("Adding a Checkpoint System requires more manual");
					GUILayout.Label("work and some coding. Please go through the step");
					GUILayout.Label("by step guide in the pdf to understand what needs");
					GUILayout.Label("to be done.");
					GUILayout.Label("");
					if (GUILayout.Button("Add Script")) {
						myTarget.gameObject.AddComponent<RPS_Checkpoints>();
						myTarget.insidePart = 0;
					}
					GUILayout.Label("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				RPS_Lap lapSystemm = myTarget.gameObject.GetComponent<RPS_Lap> ();
				if (myTarget.insidePart == 6) {
					if (lapSystemm != null) {
						myTarget.insidePart = 0;
					}
					GUILayout.Label("Add Lap Script:");
					GUILayout.Label ("");
					myTarget.lapHasEnd = EditorGUILayout.Toggle("Race has End?", myTarget.lapHasEnd);
					if (myTarget.lapHasEnd == true) {
						myTarget.lapFirstLap = EditorGUILayout.IntField("First Lap Number:", myTarget.lapFirstLap);
						myTarget.lapLastLap = EditorGUILayout.IntField("Last Lap Number:", myTarget.lapLastLap);
						GUILayout.Label ("The following toggle will freeze the race position");
						GUILayout.Label ("shown when the race has ended, always showing the");
						GUILayout.Label ("finished position.");
						myTarget.lapFreezePos = EditorGUILayout.Toggle("Freeze Position?", myTarget.lapFreezePos);
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_Lap addedLapScript = myTarget.gameObject.AddComponent<RPS_Lap>();
						addedLapScript.hasEnd = myTarget.lapHasEnd;
						addedLapScript.lastLap = myTarget.lapLastLap;
						addedLapScript.startLapNumber = myTarget.lapFirstLap;
						addedLapScript.freezeShownPosAtEnd = myTarget.lapFreezePos;
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 7) {
					if (lapSystemm == null) {
						myTarget.insidePart = 0;
					}
					GUILayout.Label("Add Lap UI Text:");
					GUILayout.Label ("");
					GUILayout.Label ("UI Text:");
					myTarget.lapUIText = (UnityEngine.UI.Text)EditorGUILayout.ObjectField(myTarget.lapUIText, typeof(UnityEngine.UI.Text), true);
					GUILayout.Label("");
					GUILayout.Label("Show the total number of laps?");
					GUILayout.Label ("e.g. show '1/3' instead of just '1'");
					myTarget.lapUIShowTotalLaps = EditorGUILayout.Toggle(myTarget.lapUIShowTotalLaps);
					GUILayout.Label ("");
					GUILayout.Label ("Change text when Finished Race?");
					myTarget.lapUIchangewhenfinished = EditorGUILayout.Toggle(myTarget.lapUIchangewhenfinished);
					if (myTarget.lapUIchangewhenfinished == true) {
						myTarget.lapchangeToText = EditorGUILayout.TextField("Change to:", myTarget.lapchangeToText);
						myTarget.lapchangeFontSize = EditorGUILayout.Toggle ("Change Font Size?", myTarget.lapchangeFontSize);
						if (myTarget.lapchangeFontSize == true) {
							myTarget.lapnewFontSize = EditorGUILayout.IntField("Font Size:", myTarget.lapnewFontSize);
						}
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_LapUI newlapUIScript = myTarget.gameObject.AddComponent<RPS_LapUI>();
						newlapUIScript.changeFontSize = myTarget.lapchangeFontSize;
						newlapUIScript.changeToText = myTarget.lapchangeToText;
						newlapUIScript.changeWhenFinished = myTarget.lapUIchangewhenfinished;
						newlapUIScript.textObj = myTarget.lapUIText;
						newlapUIScript.showTotalLaps = myTarget.lapUIShowTotalLaps;
						newlapUIScript.newFontSize = myTarget.lapnewFontSize;
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
				if (myTarget.insidePart == 8) {
					if (lapSystemm == null) {
						myTarget.insidePart = 0;
					}
					GUILayout.Label("Add Lap UI Text (Legacy):");
					GUILayout.Label ("");
					GUILayout.Label ("GUIText:");
					myTarget.llapUIText = (GUIText)EditorGUILayout.ObjectField(myTarget.llapUIText, typeof(GUIText), true);
					GUILayout.Label("");
					GUILayout.Label("Show the total number of laps?");
					GUILayout.Label ("e.g. show '1/3' instead of just '1'");
					myTarget.llapUIShowTotalLaps = EditorGUILayout.Toggle(myTarget.llapUIShowTotalLaps);
					GUILayout.Label ("");
					GUILayout.Label ("Change text when Finished Race?");
					myTarget.llapUIchangewhenfinished = EditorGUILayout.Toggle(myTarget.llapUIchangewhenfinished);
					if (myTarget.llapUIchangewhenfinished == true) {
						myTarget.llapchangeToText = EditorGUILayout.TextField("Change to:", myTarget.llapchangeToText);
						myTarget.llapchangeFontSize = EditorGUILayout.Toggle ("Change Font Size?", myTarget.llapchangeFontSize);
						if (myTarget.llapchangeFontSize == true) {
							myTarget.llapnewFontSize = EditorGUILayout.IntField("Font Size:", myTarget.llapnewFontSize);
						}
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Add")) {
						RPS_LegacyLapUI newlapUIScriptt = myTarget.gameObject.AddComponent<RPS_LegacyLapUI>();
						newlapUIScriptt.changeFontSize = myTarget.llapchangeFontSize;
						newlapUIScriptt.changeToText = myTarget.llapchangeToText;
						newlapUIScriptt.changeWhenFinished = myTarget.llapUIchangewhenfinished;
						newlapUIScriptt.textObj = myTarget.llapUIText;
						newlapUIScriptt.showTotalLaps = myTarget.llapUIShowTotalLaps;
						newlapUIScriptt.newFontSize = myTarget.llapnewFontSize;
						myTarget.insidePart = 0;
					}
					GUILayout.Label ("");
					if (GUILayout.Button("Back")) {
						myTarget.insidePart = 0;
					}
				}
			}
		}
		if (myTarget.part == 1) {
			myTarget.posScript = myTarget.gameObject.GetComponent<RPS_Position> ();
			if (myTarget.posScript == null) {
				GUILayout.Label("This gameObject needs a RPS_Position script");
				GUILayout.Label("to be used in a race.");
			} else {
				myTarget.part = 0;
			}
		}
		if (myTarget.part == 2) {
			myTarget.storageObj = GameObject.Find ("RPS_Storage");
			if (myTarget.storageObj == null) {
				GUILayout.Label("There is no RPS_Storage object in the scene.");
				GUILayout.Label("Use the Editor Window to create one.");
			} else {
				myTarget.part = 0;
			}
		}
		if (myTarget.part == 3) {
			myTarget.storageScript = myTarget.storageObj.gameObject.GetComponent<RPS_Storage>();
			if (myTarget.storageScript == null) {
				GUILayout.Label("The RPS_Storage object doesn't have a RPS_Storage");
				GUILayout.Label("script. It has not been setup correctly.");
				GUILayout.Label ("Use the Editor Window to do this properly.");
			} else {
				myTarget.part = 0;
			}
		}
		Repaint();
	}
}
