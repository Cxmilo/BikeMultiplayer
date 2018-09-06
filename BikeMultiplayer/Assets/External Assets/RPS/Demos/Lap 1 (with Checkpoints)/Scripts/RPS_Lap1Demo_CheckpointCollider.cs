/*
Race Positioning System by Solution Studios

Script Name: RPS_Lap1Demo_CheckpointCollider.cs

Description:
Used in the lap 1 demo scene, attached to the player object.
Detects trigger collisions with checkpoints and calls function on the RPS_Checkpoints script to say when checkpoint is passed.
*/

using UnityEngine;
using System.Collections;

public class RPS_Lap1Demo_CheckpointCollider : MonoBehaviour {

	//The checkpoints script on the player's object
	public RPS_Checkpoints checkpointScript;

	//Function called when any trigger event takes place through the rigidbody attached
	void OnTriggerEnter (Collider collider) {

		//Get the checkpoint number script attached to the checkpoint's collider
		RPS_Lap1Demo_CheckpointNumber checkpointNumberScript = collider.gameObject.GetComponent<RPS_Lap1Demo_CheckpointNumber>();

		if (checkpointNumberScript != null) { //make sure there was one attached

			//Get the checkpoint number from the script
			int checkpointNum = checkpointNumberScript.checkpointNumber;

			//Call funtion on the RPS_Checkpoints script to say checkpoint passed and providing the checkpoint number as a parameter
			checkpointScript.CheckpointPassed(checkpointNum);

			/*
			If you are using javascript, you can use send message:
			gameObject.SendMessage("CheckpointPassed", checkpointNum);
			*/
		}
	}
}
