
/*
Race Positioning System by Solution Studios

Script Name: RPS_Lap1Demo_AI.cs

Description:
Used in the Lap 1 Demo scene.
Attached to each human AI to move towards waypoints.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RPS_Lap1Demo_AI : MonoBehaviour {

	//The array of waypoints
	public List<Transform> waypoints = new List<Transform>();

	//The next waypoint number as an index of the waypoints array
	int nextWaypointIndex = 0;

	void Update () {
		//All of the movement uses root motion of animations so no movement code needed

		//Rotate to face the next waypoint
		Transform nextPoint = waypoints[nextWaypointIndex];
		Vector3 nextPointVect = new Vector3(nextPoint.position.x, transform.position.y, nextPoint.position.z);
		transform.LookAt(nextPointVect);

		//Check when reached waypoint
		if (Vector3.Distance(transform.position, nextPoint.transform.position) < 0.5) {
			nextWaypointIndex = nextWaypointIndex + 1;
			if (nextWaypointIndex == waypoints.Count) {
				nextWaypointIndex = 0;
			}
		}

	}
}
