/*
Race Positioning System by Solution Studios

Script Name: RPS_ScreenUI.cs

Description:
This script can be attached to any object with a RPS_Position script.
It is used to display the current race position of an object through different textures (showing 1st, 2nd, 3rd etc) or text through an object with a Unity UI Image or Text component.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum type {
	Image = 0,
	Text = 1
}

public class RPS_ScreenUI : MonoBehaviour {

	public bool spriteElementsExpand = false; //Used in the Inspector window for the number of the fields for the array.
	public int spriteElementsSize = 1;

	public type index = type.Image; //0 = image, 1 = text.     Is the script for an Image or Text component

    //Variables used if it is for an Image component
	public UnityEngine.UI.Image positionImage;
	public List<Sprite> raceSprites = new List<Sprite> (); //Array of Textures

    //Variables used if it is for a Text component
	public UnityEngine.UI.Text textUI;
	public bool textEndingEnabled = true;
	
    //The RPS_Position script on this object which the race position comes from
	private RPS_Position posScript;
	
	void Start () {
		posScript = gameObject.GetComponent<RPS_Position>(); //Assign posScript
		if (posScript == null) {
			Debug.Log ("RPS Error: RPS_Position script not found on this object. It is required by RPS_ScreenUI.");
		}
	}
	
	void Update () {
		
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Assign posScript
            if (posScript == null) {
				Debug.Log ("RPS Error: RPS_Position script not found on this object. It is required by RPS_ScreenUI.");
			}
		}
		
		if (posScript != null) {
			if (index == type.Image) {
                //if the script is for an Image component, make sure there are the correct number of Sprites in the array for all the different race positions
				int numberNeeded = posScript.storageScript.positionScript.Count;
				if (raceSprites.Count < numberNeeded) {
					Debug.Log("RPS Error: RPS_ScreenUI script does not have enough raceSprites assigned.");
				} else {
					positionImage.sprite = raceSprites[posScript.currentRacePosition]; //assigns the correct sprite to the Image component based on the race position from the RPS_Position script
				}
			} else {
                //if the script is for a Text component
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
										textUI.text = "" + (posScript.currentRacePosition+1) + "th"; //Assign the string to the Text component with the 'th' ending
                                    }
								}
							}
						}
					}
				} else {
                    //if endings are disabled, just assign the string to the Text component without an ending
					textUI.text = "" + (posScript.currentRacePosition+1);
				}
			}
		}
	}
}
