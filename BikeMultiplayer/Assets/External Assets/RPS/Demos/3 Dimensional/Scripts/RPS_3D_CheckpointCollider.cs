
/*
Race Positioning System by Solution Studios

Script Name: RPS_3D_CheckpointCollider.cs

Description:
Used in the 3D demo scene, attached to the player's aircraft object.
Detects trigger collisions with checkpoints and calls function on the RPS_Checkpoints script to say when checkpoint is passed.
Also detects trigger with finish line to call function on manager script for race finished to show finished UI
*/

using UnityEngine;
using System.Collections;

public class RPS_3D_CheckpointCollider : MonoBehaviour {

	//The script which show the UI at the end of the race
	public RPS_3D_EndOfRace managerScript;

	//The checkpoints script on the player's aircraft
	public RPS_Checkpoints checkpointScript;

	//Function called when any trigger event takes place through the rigidbody attached
	void OnTriggerEnter (Collider collider) {

		//Get the checkpoint number script attached to the checkpoint's collider
		RPS_3D_CheckpointNumber checkpointNumberScript = collider.gameObject.GetComponent<RPS_3D_CheckpointNumber>();

		if (checkpointNumberScript != null) { //make sure there was one attached

			//Get the checkpoint number from the script
			int checkpointNum = checkpointNumberScript.checkpointNumber;

			if (checkpointNum == 11) { //Trigger with finish line not checkpoint -> call function to end race

				//Call function
				managerScript.RaceFinished();

			} else {

				//Call funtion on the RPS_Checkpoints script to say checkpoint passed and providing the checkpoint number as a parameter
				checkpointScript.CheckpointPassed(checkpointNum);

				/*
				If you are using javascript, you can use send message:
				gameObject.SendMessage("CheckpointPassed", checkpointNum);
				*/
			}
		}

	}
		
}
