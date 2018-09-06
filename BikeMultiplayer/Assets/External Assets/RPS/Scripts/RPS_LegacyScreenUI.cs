/*
Race Positioning System by Solution Studios

Script Name: RPS_LegacyScreenUI.cs

Description:
This script can be attached to any object with a RPS_Position script. 
It is used to display the current race position of an object through different textures (showing 1st, 2nd, 3rd etc) or text through an object with a Legacy GUITexture or GUIText component.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPS_LegacyScreenUI : MonoBehaviour {

    public bool texturesElementsExpand = false; //Used in the Inspector window for the number of the fields for the array.
    public int texturesElementsSize = 1;

    public type index = 0; //0 = image, 1 = text.     Is the script for an Image or Text component

    //Variables used if it is for an GUITexture component
    public GUITexture positionImage;
	public List<Texture> raceTextures = new List<Texture> ();

    //Variables used if it is for a GUIText component
    public GUIText textUI;
	public bool textEndingEnabled = true;

    //The RPS_Position script on this object which the race position comes from
    private RPS_Position posScript;
	
	void Start () {
		posScript = gameObject.GetComponent<RPS_Position>(); //Assign posScript
        if (posScript == null) {
			Debug.Log ("RPS Error: RPS_Position script not found on this object. It is required by RPS_LegacyScreenUI.");
		}
	}
	
	void Update () {
		
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Assign posScript
            if (posScript == null) {
				Debug.Log ("RPS Error: RPS_Position script not found on this object. It is required by RPS_LegacyScreenUI.");
			}
		}
		
		if (posScript != null) {
			if (index == 0) {
                //if the script is for an GUITexture component, make sure there are the correct number of Textures in the array for all the different race positions
                int numberNeeded = posScript.storageScript.positionScript.Count;
				if (raceTextures.Count < numberNeeded) {
					Debug.Log("RPS Error: RPS_LegacyScreenUI script does not have enough raceTextures assigned.");
				} else {
					positionImage.texture = raceTextures[posScript.currentRacePosition];
				}
			} else {
                //if the script is for a GUIText component
                if (textEndingEnabled == true) {
                    //if endings are enabled
                    //get the race position and work out which ending should be added e.g. 'st', 'nd' or 'rd'
                    string postionnumberasstring = "" + (posScript.currentRacePosition + 1);
                    string lastfew = "0";
					if (("" + postionnumberasstring[postionnumberasstring.Length-1]) == "1") { //If race position ends in a 1
						textUI.text = "" + (posScript.currentRacePosition+1) + "st"; //Assign the string to the Text component with the 'st' ending
                    } else {
						if (postionnumberasstring.Length > 1) {
                            //Gets the last 2 digets of the race position
                            lastfew = "" + postionnumberasstring[postionnumberasstring.Length-2] + postionnumberasstring[postionnumberasstring.Length-1];
						}
						if (lastfew == "12") {
							textUI.text = "" + (posScript.currentRacePosition+1) + "th"; //Assign the string to the Text component with the 'th' ending
                        } else {
							if (lastfew == "13") {
								textUI.text = "" + (posScript.currentRacePosition+1) + "th"; //Assign the string to the Text component with the 'th' ending
                            } else {
								if (("" + postionnumberasstring[postionnumberasstring.Length-1]) == "2") {
									textUI.text = "" + (posScript.currentRacePosition+1) + "nd"; //Assign the string to the Text component with the 'nd' ending
                                } else {
									if (("" + postionnumberasstring[postionnumberasstring.Length-1]) == "3") {
										textUI.text = "" + (posScript.currentRacePosition+1) + "rd"; //Assign the string to the Text component with the 'rd' ending
                                    } else {
										textUI.text = "" + (posScript.currentRacePosition+1) + "th";  //Assign the string to the Text component with the 'th' ending
                                    }
								}
							}
						}
					}
				} else {
                    //if endings are disabled, just assign the string to the GUIText component without an ending
                    textUI.text = "" + (posScript.currentRacePosition+1);
				}
			}
		}
	}
}