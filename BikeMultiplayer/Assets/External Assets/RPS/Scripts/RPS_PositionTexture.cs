/*
Race Positioning System by Solution Studios

Script Name: RPS_PositionTexture.cs

Description:
This script can be attached to any object with a RPS_Position script.
It is used to display the current race position of an object through different textures (showing 1st, 2nd, 3rd etc) through an object’s renderer.
E.g. A plane or quad might be put above a car to show its position.
This script changes the texture on the plane to show the car’s position.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPS_PositionTexture : MonoBehaviour {

    public bool textureElementsExpand = false; //Used in the Inspector window for the number of the fields for the array.
    public int textureElementsSize = 1;

    public Renderer planeRenderer; //The renderer to change the texture on
	public List<Texture> raceTextures = new List<Texture> (); //List of textures for the different race positions

    //The RPS_Position script on this object which the race position comes from
    private RPS_Position posScript;

	void Start () {
		posScript = gameObject.GetComponent<RPS_Position>(); //Assign posScript
        if (posScript == null) {
			Debug.Log ("RPS Error: RPS_Position script not found on this object. It is required by RPS_PositionTexture.");
		}
	}

	void Update () {

		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Assign posScript
            if (posScript == null) {
				Debug.Log ("RPS Error: RPS_Position script not found on this object. It is required by RPS_PositionTexture.");
			}
		}

		if (posScript != null) {
			int numberNeeded = posScript.storageScript.positionScript.Count;
			if (raceTextures.Count < numberNeeded) {
                //make sure there are the correct number of Textures in the array for all the different race positions
                Debug.Log("RPS Error: RPS_PositionTexture script does not have enough raceTextures assigned.");
			} else {
                //Assign the correct texture based on the current race position from the RPS_Position script
				planeRenderer.material.mainTexture = raceTextures[posScript.currentRacePosition];
			}
		}
	}
}
