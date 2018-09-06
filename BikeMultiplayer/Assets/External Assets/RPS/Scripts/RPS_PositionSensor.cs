/*
Race Positioning System by Solution Studios

Script Name: RPS_PositionSensor.cs

Description:
This script is attached to all PositionSensors in the race scene (when you set them up in the editor window).
It stores the position of the PositionSensor in relation to the other PositionSensors before and after it.
The RPS_Position scripts constantly find the nearest RPS_PositionSensor script in order to calculate the current race position.
*/

using UnityEngine;
using System.Collections;

public class RPS_PositionSensor : MonoBehaviour {

    //This script just stores some basic variables for each PositionSensor
    //All of the variables are used by RPS_Position scripts to calculate their position and compare it to other RPS_Position scripts' postions to calculate the overal race position of that object

    //This PositionSensors position number
	public float thisPosition = 0.0f;

    //Variables relating it to the other PositionSensors and their numbers
	public bool isFirst = false;
	public bool isLast = false;
	public float lastPosition = 0.0f;
	public float nextPosition = 0.0f;

	void OnDrawGizmos () {
		Color col = Color.red;
		col.a = 0.6f;
		Gizmos.color = col;
		Gizmos.DrawSphere(transform.position, 0.1f);
	}
	
}
