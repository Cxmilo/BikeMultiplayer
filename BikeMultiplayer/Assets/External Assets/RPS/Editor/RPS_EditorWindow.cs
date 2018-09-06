/*
Race Positioning System by Solution Studios

Script Name: RPS_EditorWindow.cs

Description:
This script is an editor script.
*/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class RPS_EditorWindow : EditorWindow
{

    int part = 0;
    bool tempDone = false;
    Vector2 scrollPosOne = new Vector2(0, 0);
    float tempTrackWidth = 5.0f;
    int rectNumber = 70;
	GameObject tempPos;
	public raceDimensions tempDimensions = raceDimensions.Two_Dimensions;
	public raceType tempRaceType = raceType.Use_Laps;
	GameObject storageobj;
	RPS_Storage storagescript;
	bool isFirstFrame = true;

	public enum raceType
	{
		Use_Laps = 0,
		Point_To_Point = 1
	}

	public enum raceDimensions
	{
		Two_Dimensions = 0,
		Three_Dimensions = 1,
	}

    [MenuItem("Window/RPS Editor Window")]
    static void Init()
    {
        RPS_EditorWindow window = (RPS_EditorWindow)EditorWindow.GetWindow(typeof(RPS_EditorWindow));
        window.name = "RPS Editor";
		window.titleContent = new GUIContent("RPS_Editor");
        window.maxSize = new Vector2(400,600);
        window.minSize = window.maxSize;
        window.Show();
    }

    void OnGUI ()
    {
        Texture2D black = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        black.SetPixel(0, 0, Color.black);
        black.Apply();
        GUI.DrawTexture(new Rect(0, 0, 400, 153), Resources.Load("EditorTitle") as Texture, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(8, 565, 384, 30), Resources.Load("EditorBack") as Texture, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 0, 400, 8), black, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 145, 400, 8), black, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 0, 8, 600), black, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(392, 0, 8, 600), black, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 592, 400, 8), black, ScaleMode.StretchToFill);
        GUI.DrawTexture(new Rect(0, 564, 400, 8), black, ScaleMode.StretchToFill);
        GUILayout.BeginArea(new Rect(8, 573, 384, 20));
        GUILayout.BeginHorizontal();
        if (GUILayout.Button ("Website"))
        {
            Application.OpenURL("http://solution-studios-for-unity.moonfruit.com/");
        }
        if (GUILayout.Button("Forum"))
        {
            Application.OpenURL("http://forum.unity3d.com/threads/race-positioning-system.395526/");
        }
        if (GUILayout.Button("Contact Us"))
        {
            Application.OpenURL("http://solution-studios-for-unity.moonfruit.com/contact-us/4583955951");
        }
        if (GUILayout.Button("Asset Store Page"))
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/17804");
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
        GUILayout.BeginArea(new Rect(8, 159, 384, 397));
        scrollPosOne = EditorGUILayout.BeginScrollView(scrollPosOne);
        if (storageobj == null)
        {
            storageobj = GameObject.Find("RPS_Storage");
        }
        if (storageobj == null)
        {
            part = 0;
        } else
        {
            if (storagescript == null)
            {
                storagescript = storageobj.gameObject.GetComponent<RPS_Storage>();
            }
            if (storagescript == null)
            {
                part = 0;
            } else
            {
                if (storagescript.PositionSensorsSetup == false)
                {
                    if (storagescript.hasAddedPSScript == true)
                    {
                        if (part != 3)
                        {
                            part = 2;
                        }
                    }
                    else
                    {
						if (storagescript.hasRemovedPSScript == false) {
                       		part = 1;
						} else {
							if ((part != 4)&&(part != 173)) {
								part = 3;
							}
						}
                    }
                } else
                {
					if (isFirstFrame == true) {
                 	   part = 4;
					}
                }
            }
        }
		if (isFirstFrame == true) {
			isFirstFrame = false;
		}
        if (part == 0)
        {
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Setup Storage Object:");
            GUI.skin.label.fontSize = 11;
            GUILayout.Label("");
            GUILayout.Label("");
            GUILayout.Label("There is no RPS_Storage object in the scene.");
            GUILayout.Label("A storage object is required to store information about a race.");
            GUILayout.Label("");
            GUILayout.Label("Select a race type:");
            tempRaceType = (raceType)EditorGUILayout.EnumPopup(tempRaceType);
            GUILayout.Label("");
            if (GUILayout.Button("Create RPS_Storage Object"))
            {
                storageobj = new GameObject("RPS_Storage");
                storagescript = storageobj.gameObject.AddComponent<RPS_Storage>();
                if (tempRaceType == raceType.Point_To_Point)
                {
                    storagescript.isPointToPoint = true;
                } else
                {
                    storagescript.isPointToPoint = false;
                }
                part = 1;
            }
        }
        if (part == 1)
        {
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Setup PositionSensors:");
            GUI.skin.label.fontSize = 11;
            GUILayout.Label("");
            GUILayout.Label("");
            GUILayout.Label("First, you need to place PositionSensors around/along your track");
            GUILayout.Label("This enables each car to find the nearest PositionSensor telling");
            GUILayout.Label("it, it's position around the track.");
            GUILayout.Label("");
            GUILayout.Label("");
            if (GUILayout.Button("Start"))
            {
                tempDone = false;
                tempDimensions = raceDimensions.Two_Dimensions;
                tempTrackWidth = 5.0f;
                rectNumber = 20;
                RPS_PSPlacer newscript;
                newscript = storageobj.gameObject.GetComponent<RPS_PSPlacer>();
                if (newscript == null)
                {
                    newscript = storageobj.gameObject.AddComponent<RPS_PSPlacer>();
                }
                storagescript.PSScript = newscript;
                storagescript.hasAddedPSScript = true;
                foreach(GameObject obj in storagescript.pointChildren)
                {
                    if (obj != null)
                    {
                        DestroyImmediate(obj.gameObject);
                    }
                }
                storagescript.pointChildren = new List<GameObject>();
                part = 2;
            }
        }
        if (part == 2)
        {
            if (tempRaceType == raceType.Use_Laps)
            {
				if (storageobj.gameObject.GetComponent<RPS_PSPlacer>() != null) {
               		storageobj.gameObject.GetComponent<RPS_PSPlacer>().isLoop = true;
				}
            } else
            {
				if (storageobj.gameObject.GetComponent<RPS_PSPlacer>() != null) {
					storageobj.gameObject.GetComponent<RPS_PSPlacer>().isLoop = false;
				}
            }
            storageobj.gameObject.GetComponent<RPS_PSPlacer>().pointsEnabled = tempDone;
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Setup PositionSensors (Part 1):");
            GUI.skin.label.fontSize = 11;
            GUILayout.Label("");
            GUILayout.Label("");
            GUILayout.Label("First you need to create a loop/line following your track.");
            GUILayout.Label("To do this click the button bellow to create objects which will");
            GUILayout.Label("link together to create a circuit/line.");
            GUILayout.Label("");
            if (GUILayout.Button("Add Point"))
            {
                GameObject newPoint = new GameObject("Point");
                newPoint.transform.parent = storageobj.transform;
                if (storagescript.pointChildren == null)
                {
                    storagescript.pointChildren = new List<GameObject>();
                    newPoint.transform.position = Vector3.zero;
                }
                else
                {
                    if (storagescript.pointChildren.Count > 0)
                    {
                        if (storagescript.pointChildren[storagescript.pointChildren.Count - 1] != null)
                        {
                            newPoint.transform.position = storagescript.pointChildren[storagescript.pointChildren.Count - 1].gameObject.transform.position;
                        }
                        else
                        {
                            newPoint.transform.position = Vector3.zero;
                        }
                    } else
                    {
                        newPoint.transform.position = Vector3.zero;
                    }
                }
                storagescript.pointChildren.Add(newPoint);
                storageobj.gameObject.GetComponent<RPS_PSPlacer>().addPoint(newPoint.transform);
                Selection.activeGameObject = newPoint.gameObject;
            }
            GUILayout.Label("");
            GUILayout.Label("The objects are created as children of the RPS_Storage object");
            GUILayout.Label("you will find in your scene. Move the objects around to outline");
            GUILayout.Label("your track.");
            GUILayout.Label("The first point should be just infront of your start line.");
            GUILayout.Label("The last point should be just behind your finish line.");
            GUILayout.Label("If you need to delete an object, do it manually and the other");
            GUILayout.Label("objects will automatically rename and resort themselves.");
            GUILayout.Label("");
            GUILayout.Label("The line should go along the middle of your track and should");
            GUILayout.Label("be slightly raised above the ground. If your track is in 3");
            GUILayout.Label("dimensions e.g. tube-like, the line should pass through the");
            GUILayout.Label("center of the tube.");
            GUILayout.Label("");
            tempDone = EditorGUILayout.Toggle("Done", tempDone);
            if (tempDone == true)
            {
                GUILayout.Label("");
                GUILayout.Label("Now you need to select you track type.");
                GUILayout.Label("And change the width to suit it's general shape.");
                GUILayout.Label("In the Scene View, you will see red rectangles which will");
                GUILayout.Label("need to be in line with the cross-sections of your track.");
                tempTrackWidth = EditorGUILayout.FloatField("Track Width:", tempTrackWidth);
                tempDimensions = (raceDimensions)EditorGUILayout.EnumPopup("Race type:", tempDimensions);
				if (tempDimensions == raceDimensions.Two_Dimensions) {
					storagescript.is2D = true;
				} else {
					storagescript.is2D = false;
				}
                storageobj.gameObject.GetComponent<RPS_PSPlacer>().trackwidth = tempTrackWidth;
                GUILayout.Label("");
                GUILayout.Label("The rectangles represent where the PositionSensors will be");
                GUILayout.Label("created. The more Rectangles the more accurate, but the");
                GUILayout.Label("higher performance and memory costs.");
                GUILayout.Label("Increase the following variable to increase the numbe of sets");
                GUILayout.Label("of PositionSensors that will be created:");
                rectNumber = EditorGUILayout.IntField(rectNumber);
                storageobj.GetComponent<RPS_PSPlacer>().pointSubsteps = rectNumber;
                GUILayout.Label("Warning: Do not set the variable too high, it will crash your");
                GUILayout.Label("computer.");
                GUILayout.Label("");
                GUILayout.Label("Check the documentation PDF if you have any trouble.");
                GUILayout.Label("");
                if (GUILayout.Button("Next (you cannot go back to this)"))
                {
					storagescript.hasRemovedPSScript = true;
                    storagescript.PSScript.CreateCubes();
					foreach (GameObject gam in storagescript.pointChildren)
					{
						DestroyImmediate(gam.gameObject);
					}
                    part = 3;
                }
			}
			GUILayout.Label("");
            if (GUILayout.Button("Back (this will remove all you done so far)"))
            {
           	   foreach (GameObject obj in storagescript.pointChildren)
           	   {
       		   		if (obj != null)
  	            	{
						DestroyImmediate(obj.gameObject);
          	    	}
  	      	  	}
 	       		storagescript.pointChildren = new List<GameObject>();
            	DestroyImmediate(storagescript.PSScript);
            	storagescript.PSScript = null;
            	storagescript.hasAddedPSScript = false;
            	tempDone = false;
            	part = 1;
	    	}
        }
        if (part == 3)
        {
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Setup PositionSensors (Part 2):");
            GUI.skin.label.fontSize = 11;
            GUILayout.Label("");
            GUILayout.Label("");
            GUILayout.Label("In your scene, all of the rectangles have now been created as");
            GUILayout.Label("GameObjects. The editor scripts cannot always get them in");
            GUILayout.Label("the right position so you may need to adjust them manually.");
            GUILayout.Label("");
            GUILayout.Label("Carefully go around your track and check them.");
            GUILayout.Label("Make sure they do not overlap. Touching is fine but not");
            GUILayout.Label("completley overlapping.");
            GUILayout.Label("");
            GUILayout.Label("In areas where there are many, you should delete some and");
            GUILayout.Label("where there are fewer, spread them out evenly.");
            GUILayout.Label("");
            GUILayout.Label("IMPORTANT: Do not change their order and remember:");
            GUILayout.Label("The first point should be just infront of your start line.");
            GUILayout.Label("The last point should be just behind your finish line.");
			GUILayout.Label("");
			if (GUILayout.Button("Finished")) {
				int nummmmminar = 0;
				storagescript.allPositionSensors = new List<RPS_PositionSensor>();
				List<int> setNumbers = new List<int>();
				int lastPositionNumber = 0;
				int numinarr = 0;
				foreach (GameObject objthing in storagescript.cuboidarr) {
					if (objthing != null) {
						RPS_PS3D findLastScript = objthing.gameObject.GetComponent<RPS_PS3D>();
						if (findLastScript == null) {
							if (numinarr > lastPositionNumber) {
								lastPositionNumber = numinarr;
							}
						} else {
							if (findLastScript.setNumber > lastPositionNumber) {
								lastPositionNumber = findLastScript.setNumber;
							}
						}
					}
					numinarr = numinarr + 1;
				}
				foreach (GameObject objthing in storagescript.cuboidarr) {
					if (objthing != null) {
						bool shouldCreateCentre = false;
						int posOfSensors = nummmmminar;
						RPS_PS3D setScript = objthing.gameObject.GetComponent<RPS_PS3D>();
						if (setScript == null) {
							shouldCreateCentre = true;
						} else {
							int setNumber = setScript.setNumber;
							posOfSensors = setNumber;
							if (setNumbers.Contains(setNumber)) {
								shouldCreateCentre = false;
							} else {
								shouldCreateCentre = true;
								setNumbers.Add(setNumber);
							}
						}
						GameObject objOne = new GameObject("PositionSensor");
						objOne.transform.parent = objthing.transform;
						objOne.transform.localScale = new Vector3(1/objOne.transform.localScale.x, 1/objOne.transform.localScale.y, 1/objOne.transform.localScale.z);
						objOne.transform.localPosition = new Vector3(-0.5f + (1f/8f) ,0f,0f);
						RPS_PositionSensor objOneScript = objOne.gameObject.AddComponent<RPS_PositionSensor>();
						objOneScript.thisPosition = posOfSensors;
						if (posOfSensors == 0) {
							objOneScript.isFirst = true;
							objOneScript.lastPosition = objOneScript.thisPosition;
						} else {
							objOneScript.isFirst = false;
							objOneScript.lastPosition = posOfSensors - 1;
						}
						if (posOfSensors == lastPositionNumber) {
							objOneScript.isLast = true;
							objOneScript.nextPosition = objOneScript.thisPosition;
						} else {
							objOneScript.isLast = false;
							objOneScript.nextPosition = posOfSensors + 1;
						}
						GameObject objTwo = GameObject.Instantiate(objOne);
						objTwo.gameObject.transform.parent = objthing.transform;
						objTwo.name = "PositionSensor";
						objTwo.transform.localPosition = new Vector3(-0.5f + (2f/8f) ,0f,0f);
						GameObject objThree = GameObject.Instantiate(objOne);
						objThree.gameObject.transform.parent = objthing.transform;
						objThree.name = "PositionSensor";
						objThree.transform.localPosition = new Vector3(-0.5f + (3f/8f) ,0f,0f);
						GameObject objFour = GameObject.Instantiate(objOne);
						objFour.gameObject.transform.parent = objthing.transform;
						objFour.name = "PositionSensor";
						objFour.transform.localPosition = new Vector3(-0.5f + (4f/8f) ,0f,0f);
						GameObject objFive = GameObject.Instantiate(objOne);
						objFive.gameObject.transform.parent = objthing.transform;
						objFive.name = "PositionSensor";
						objFive.transform.localPosition = new Vector3(-0.5f + (5f/8f) ,0f,0f);
						GameObject objSix = GameObject.Instantiate(objOne);
						objSix.gameObject.transform.parent = objthing.transform;
						objSix.name = "PositionSensor";
						objSix.transform.localPosition = new Vector3(-0.5f + (6f/8f) ,0f,0f);
						GameObject objSeven = GameObject.Instantiate(objOne);
						objSeven.gameObject.transform.parent = objthing.transform;
						objSeven.name = "PositionSensor";
						objSeven.transform.localPosition = new Vector3(-0.5f + (7f/8f) ,0f,0f);
						DestroyImmediate(objthing.gameObject.GetComponent<Renderer>());
						DestroyImmediate(objthing.gameObject.GetComponent<Collider>());
						DestroyImmediate(objthing.gameObject.GetComponent<MeshFilter>());
						if (shouldCreateCentre == true) {
							storagescript.allPositionSensors.Add (objOne.gameObject.GetComponent<RPS_PositionSensor>());
						} else {
							DestroyImmediate(objOne.gameObject);
						}
						storagescript.allPositionSensors.Add (objTwo.gameObject.GetComponent<RPS_PositionSensor>());
						storagescript.allPositionSensors.Add (objThree.gameObject.GetComponent<RPS_PositionSensor>());
						storagescript.allPositionSensors.Add (objFour.gameObject.GetComponent<RPS_PositionSensor>());
						storagescript.allPositionSensors.Add (objFive.gameObject.GetComponent<RPS_PositionSensor>());
						storagescript.allPositionSensors.Add (objSix.gameObject.GetComponent<RPS_PositionSensor>());
						storagescript.allPositionSensors.Add (objSeven.gameObject.GetComponent<RPS_PositionSensor>());
						nummmmminar = nummmmminar + 1;
					}
				}
				foreach (GameObject objthing in storagescript.cuboidarr) {
					RPS_PS3D isScript = objthing.gameObject.GetComponent<RPS_PS3D>();
					if (isScript != null) {
						DestroyImmediate(isScript);
					}
				}
				part = 173;
				storagescript.PositionSensorsSetup = true;
			}
			GUILayout.Label("");
			if (GUILayout.Button("Back (all progress will be lost)")) {
				RPS_PSPlacer storplace = storageobj.gameObject.GetComponent<RPS_PSPlacer>();
				if (storplace != null) {
					DestroyImmediate(storplace);
				}
				foreach (GameObject objone in storagescript.cuboidarr) {
					DestroyImmediate(objone.gameObject);
				}
				foreach (GameObject objone in storagescript.pointChildren) {
					DestroyImmediate(objone.gameObject);
				}
				foreach (RPS_PositionSensor objone in storagescript.allPositionSensors) {
					DestroyImmediate(objone.gameObject);
				}
				storagescript.cuboidarr = new List<GameObject>();
				storagescript.pointChildren = new List<GameObject>();
				storagescript.allPositionSensors = new List<RPS_PositionSensor>();
				storagescript.firstPositionNumber = 0.0f;
				storagescript.lastPositionNumber = 0.0f;
				storagescript.PositionSensorsSetup = false;
				storagescript.hasAddedPSScript = false;
				storagescript.hasRemovedPSScript = false;
				if (storagescript.PSScript != null) {
					DestroyImmediate(storagescript.PSScript);
				}
				tempDone = false;
				tempTrackWidth = 5.0f;
				rectNumber = 70;
				tempDimensions = raceDimensions.Two_Dimensions;
				part = 2;
			}
        }
		if (part == 173) {
			GUI.skin.label.fontSize = 20;
			GUILayout.Label("Setup PositionSensors Finished:");
			GUI.skin.label.fontSize = 11;
			GUILayout.Label("");
			GUILayout.Label("");
			GUILayout.Label("All PositionSensors are now setup.");
			GUILayout.Label("Now you need to identify the objects involved in the race.");
			GUILayout.Label("");
			if (GUILayout.Button("Continue")) {
				part = 4;
			}
		}
        if (part == 4)
        {
            GUI.skin.label.fontSize = 20;
            GUILayout.Label("Menu:");
            GUI.skin.label.fontSize = 11;
            GUILayout.Label("");
            GUILayout.Label("");
            if (GUILayout.Button("Add Position Sensing to Object"))
            {
				part = 5;
            }
            if (GUILayout.Button("Redo PositionSensors"))
            {
				if (EditorUtility.DisplayDialog("RPS: Redo PositionSensors", "All PositionSensors will be removed from the scene. Are you sure you want to continue?", "Yes", "No")) {
					foreach (RPS_PositionSensor sensor in storagescript.allPositionSensors) {
						DestroyImmediate(sensor.gameObject);
					}
					storagescript.allPositionSensors = new List<RPS_PositionSensor>();
					foreach (GameObject cubeoid in storagescript.cuboidarr) {
						DestroyImmediate(cubeoid.gameObject);
					}
					storagescript.cuboidarr = new List<GameObject>();
					storagescript.firstPositionNumber = 0.0f;
					storagescript.lastPositionNumber = 0.0f;
					storagescript.PositionSensorsSetup = false;
					storagescript.hasAddedPSScript = false;
					storagescript.hasRemovedPSScript = false;
					storagescript.PSScript = null;
					foreach (GameObject child in storagescript.pointChildren) {
						DestroyImmediate(child.gameObject);
					}
					storagescript.pointChildren = new List<GameObject>();
					part = 1;
				}
            }
            if (GUILayout.Button("Remove all RPS Stuff from Scene"))
            {
				if (EditorUtility.DisplayDialog("RPS: Remove from scene", "Are you sure you want to remove all RPS components from this scene?", "Yes", "No")) {
					DestroyImmediate(storageobj.gameObject);
					RPS_Position[] scriptOne = FindObjectsOfType(typeof(RPS_Position)) as RPS_Position[];
					RPS_Checkpoints[] scriptTwo = FindObjectsOfType(typeof(RPS_Checkpoints)) as RPS_Checkpoints[];
					RPS_Inspector[] scriptThree = FindObjectsOfType(typeof(RPS_Inspector)) as RPS_Inspector[];
					RPS_Lap[] scriptFour = FindObjectsOfType(typeof(RPS_Lap)) as RPS_Lap[];
					RPS_LapUI[] scriptFive = FindObjectsOfType(typeof(RPS_LapUI)) as RPS_LapUI[];
					RPS_LegacyLapUI[] scriptSix = FindObjectsOfType(typeof(RPS_LegacyLapUI)) as RPS_LegacyLapUI[];
					RPS_LegacyScreenUI[] scriptSeven = FindObjectsOfType(typeof(RPS_LegacyScreenUI)) as RPS_LegacyScreenUI[];
					RPS_PositionSensor[] scriptEight = FindObjectsOfType(typeof(RPS_PositionSensor)) as RPS_PositionSensor[];
					RPS_PositionTexture[] scriptNine = FindObjectsOfType(typeof(RPS_PositionTexture)) as RPS_PositionTexture[];
					RPS_PSPlacer[] scriptTen = FindObjectsOfType(typeof(RPS_PSPlacer)) as RPS_PSPlacer[];
					RPS_ScreenUI[] scriptEleven = FindObjectsOfType(typeof(RPS_ScreenUI)) as RPS_ScreenUI[];
					RPS_Storage[] scriptTwelve = FindObjectsOfType(typeof(RPS_Storage)) as RPS_Storage[];
					foreach (RPS_Position script in scriptOne) {
						DestroyImmediate(script);
					}
					foreach (RPS_Checkpoints script in scriptTwo) {
						DestroyImmediate(script);
					}
					foreach (RPS_Inspector script in scriptThree) {
						DestroyImmediate(script);
					}
					foreach (RPS_Lap script in scriptFour) {
						DestroyImmediate(script);
					}
					foreach (RPS_LapUI script in scriptFive) {
						DestroyImmediate(script);
					}
					foreach (RPS_LegacyLapUI script in scriptSix) {
						DestroyImmediate(script);
					}
					foreach (RPS_LegacyScreenUI script in scriptSeven) {
						DestroyImmediate(script);
					}
					foreach (RPS_PositionSensor script in scriptEight) {
						DestroyImmediate(script);
					}
					foreach (RPS_PositionTexture script in scriptNine) {
						DestroyImmediate(script);
					}
					foreach (RPS_PSPlacer script in scriptTen) {
						DestroyImmediate(script);
					}
					foreach (RPS_ScreenUI script in scriptEleven) {
						DestroyImmediate(script);
					}
					foreach (RPS_Storage script in scriptTwelve) {
						DestroyImmediate(script);
					}
				}
            }
			GUILayout.Label("");
			if (storagescript.isPointToPoint == false) {
				GUILayout.Label("If you want a lap system or checkpoint system, they must be");
				GUILayout.Label("added to each position sensing object individually in the");
				GUILayout.Label("Inspector.");
			} else {
				GUILayout.Label("If you want a checkpoint system, it must be added to each");
				GUILayout.Label("position sensing object individually in the Inspector");
			}
			GUILayout.Label("");
			GUILayout.Label("For more information look at the PDFs included.");
        }
		if (part == 5) {
			GUI.skin.label.fontSize = 20;
			GUILayout.Label("Add Position Sensing to Object:");
			GUI.skin.label.fontSize = 11;
			GUILayout.Label("");
			GUILayout.Label("");
			GUILayout.Label("Object: e.g. Car, airplane, etc");
			tempPos = (GameObject)EditorGUILayout.ObjectField(tempPos, typeof(GameObject), true);
			GUILayout.Label("");
			if (GUILayout.Button ("Add")) {
				RPS_Position attempt = tempPos.gameObject.GetComponent<RPS_Position>();
				if (attempt == null) {
					tempPos.gameObject.AddComponent<RPS_Inspector>();
					RPS_Position posSc = tempPos.gameObject.AddComponent<RPS_Position>();
					storagescript.positionScript.Add(posSc);
					Selection.activeGameObject = tempPos.gameObject;
				} else {
					EditorUtility.DisplayDialog("RPS Editor Error", "The gameObject already has a RPS_Position script attached. An object should not have more than one RPS_Position script.", "OK");
				}
				tempPos = null;
			}
			GUILayout.Label("");
			GUILayout.Label("All other features such as UI and a Lap System");
			GUILayout.Label("must be added using the Inspector.");
			GUILayout.Label("");
			if (GUILayout.Button ("Back")) {
				tempPos = null;
				part = 4;
			}
		}
        EditorGUILayout.EndScrollView();
        GUILayout.EndArea();
        Repaint();
    }
}