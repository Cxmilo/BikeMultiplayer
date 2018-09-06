
/*
Race Positioning System by Solution Studios

Script Name: RPS_3D_CheckpointManager.cs

Description:
Used in the 3D demo scene, attached to the player's aircraft object.
Sets the colour of the next checkpoint to green and others to red.
Also, shows text when checkpoint has been missed.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPS_3D_CheckpointManager : MonoBehaviour {

	//The RPS_Checkpoints script attached to this object
	public RPS_Checkpoints checkScript; 

	//Array with the renderers of the checkpoints
	public List<Renderer> checkRenderers = new List<Renderer>();
	public Renderer finishLine;

	//Coloured Checkpoint materials
	public Material greenMat;
	public Material redMat;

	//The gameObject showing the 'Checkpoint Missed' text. GameObject.SetActive used to enable/disable text
	public GameObject checkMissedText;

	//Remembers whether the text is being shown or not. Using this reduces the number of necessary GameObject.SetActive calls
	private bool textIsActive = false;

	void Update () {
		
		//Check if a checkpoint has been missed
		bool hasMissed = checkScript.checkpointMissed();
		if (hasMissed == true) {
			if (textIsActive == false) {
				checkMissedText.gameObject.SetActive(true); //Show has missed text
				textIsActive = true;
			}
		} else {
			if (textIsActive == true) {
				checkMissedText.gameObject.SetActive(false); //Hide has missed text
				textIsActive = false;
			}
		}

		//Find next checkpoint number
		int nextCheckpoint = checkScript.nextCheckpointNumber();
		//check there is a next checkpoint
		bool hasNext;
		if (nextCheckpoint == -1) {
			hasNext = false;
		} else {
			hasNext = true;
		}
		//Go through array and set materials
		int checkNum = 0;
		foreach (Renderer checkRend in checkRenderers) {
			if ((nextCheckpoint == checkNum)&&(hasNext == true)) {
				checkRend.material = greenMat;
			} else {
				checkRend.material = redMat;
			}
			checkNum = checkNum + 1;
		}
		//Set material of finsh line
		if (hasNext == false) {
			finishLine.material = greenMat;
		} else {
			finishLine.material = redMat;
		}

	}

}
