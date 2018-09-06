/*
Race Positioning System by Solution Studios

Script Name: RPS_Sprint1Demo_CheckpointCollider.cs

Description:
Used in the Sprint 1 demo scene, attached to the player's aircraft object.
Detects trigger collisions with checkpoints and calls function on the RPS_Checkpoints script to say when checkpoint is passed.
*/

using UnityEngine;
using System.Collections;

public class RPS_Sprint1Demo_CheckpointCollider : MonoBehaviour {

    //The checkpoints script on the player's object
    public RPS_Checkpoints checkpointScript;

    //Script to call function on when crosses last checkpoint
    public RPS_Sprint1Demo_Finished finishedScript;

    //Function called when any trigger event takes place through the rigidbody attached
    void OnTriggerEnter(Collider collider)
    {

        //Get the checkpoint number script attached to the checkpoint's collider
        RPS_Sprint1Demo_CheckpointNumber checkpointNumberScript = collider.gameObject.GetComponent<RPS_Sprint1Demo_CheckpointNumber>();

        if (checkpointNumberScript != null)
        { //make sure there was one attached

            //Get the checkpoint number from the script
            int checkpointNum = checkpointNumberScript.checkpointNumber;

            //If gone past last checkpoint and the race position is not being limited by having missed previous checkpoints
            if ((checkpointNum == 5) && (checkpointScript.limitedPosition == Mathf.Infinity))
            {
                //call function on another script to show the finished UI
                finishedScript.RaceFinished();
            }
            else
            {

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
