
/*
Race Positioning System by Solution Studios

Script Name: RPS_3D_EndOfRace.cs

Description:
Used in the 3D demo scene.
Basic script retrieving the finished position once the race has finished.
Shows 2 different UIs for the different finished positions.
*/

using UnityEngine;
using System.Collections;

public class RPS_3D_EndOfRace : MonoBehaviour {

	//The RPS_Position script attached to the player's plane
	public RPS_Position playerPosScript;

	//The gameObjects containing all of the finished 1st and 2nd UI as children
	public GameObject finishFirstUI;
	public GameObject finishSecondUI;

	//The restart button which no longer needs to be visible
	public GameObject restartButton;

	//Finished Race Position
	private int racePos;

	//bool to make sure raceFinished is only run once to reduce calls
	private bool hasFinished = false;

	//Called by script attached to the finish line collider
	public void RaceFinished () {

		if (hasFinished == false) {
			hasFinished = true;

			//Get the race position
			racePos = playerPosScript.getRacePosition();
			//Tell the RPS_Position script the race has finished so it is ahead of unfinished cars
			playerPosScript.raceFinished();
			//Freeze the race position as the race has ended
			playerPosScript.freezePosition();

			//Show the relavent UI based on the race position
			if (racePos == 0) {
				//Finished 1st
				StartCoroutine("finishedFirst");
			} else {
				//Finished 2nd
				StartCoroutine("finishedSecond");
			}
		}
	}

	//Called when race is finished and finished first
	IEnumerator finishedFirst () {
		//Add small delay
		yield return new WaitForSeconds(4);
		//Show UI
		finishFirstUI.gameObject.SetActive(true);
		//Remove restart button
		restartButton.gameObject.SetActive(false);
		//Freeze time
		Time.timeScale = 0;
	}

	//Called when race is finished and finished second
	IEnumerator finishedSecond () {
		//Add small delay
		yield return new WaitForSeconds(4);
		//Show UI
		finishSecondUI.gameObject.SetActive(true);
		//Remove restart button
		restartButton.gameObject.SetActive(false);
		//Freeze time
		Time.timeScale = 0;
	}
}
