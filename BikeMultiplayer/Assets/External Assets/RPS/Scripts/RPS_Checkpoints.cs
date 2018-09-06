/*
Race Positioning System by Solution Studios

Script Name: RPS_Checkpoints.cs

Description:
This script must be attached to each object with an RPS_Position script which you want a checkpoint system to apply to.
It stores lists of checkpoints and whether they have been passed or not.
The RPS_Position script can then access this script to limit the known position around the track to any checkpoints which may have been skipped.
The 'CheckpointPassed' function on this script must be called by whatever method you choose when a checkpoint is passed.
*/

using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Linq;
using System.Collections.Generic;

public class RPS_Checkpoints : MonoBehaviour {

	[System.Serializable]
	public class checkpoint { //Each checkpoint has two variables
		public float limitNumber; //The nearest position sensor to the checkpoint. The RPS_Position script cannot be further around the track than this without having passed the checkpoint.
		public bool passedOnStartLap; //Whether the checkpoint has been passed on the starting lap or not
	}

	[SerializeField]
	public List<checkpoint> checkpoints = new List<checkpoint>(); //List of checkpoints set in the inspector

	public class checkpointLap { //A set of checkpoints in a lap with a boolean for whether they have been passed or not
		public int lapNumber;
		public List<bool> hasPassed = new List<bool>();
	}

	public List<checkpointLap> checkpointLaps = new List<checkpointLap>(); //Sets of checkpoints for each lap. This way if the player goes back a lap, the checkpoints on the furthest lap can still be remembered.

	public RPS_Position posScript; //The RPS_Position script attached to the object

	public float limitedPosition = Mathf.Infinity; //The position number of the next checkpoint which the current position number must be bellow
	//The RPS_Position script can access this to limit the position if necessary

	void Start () {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
		} else {
			//Add the first laps checkpoints to the array of all checkpoints
			checkpointLap firstLap = new checkpointLap();
			firstLap.lapNumber = posScript.currentLapNumber - posScript.lapsGoneBack;
			List<bool> firstLapsCheckpointsPassed = new List<bool>();
			foreach (checkpoint point in checkpoints) {
				firstLapsCheckpointsPassed.Add(point.passedOnStartLap); //Add the checkpoints and whether they have been passed on initial lap from the checkpoint array
			}
			firstLap.hasPassed = firstLapsCheckpointsPassed;
			checkpointLaps.Add(firstLap); //Add the lap's checkpoints to the checkpointLaps array
		}
	}

	public void Update () {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
		} else {
			//check all checkpoints ahead of a not passed checkpoint have not been passed
			bool reachedCheckpointWhichHasntBeenPassed = false;
			int lapIndex = 0;
			foreach (checkpointLap checkLap in checkpointLaps) {
				int checkIndex = 0;
				foreach (bool checkPassed in checkLap.hasPassed) {
					if (checkPassed == false) {
						//hasn't passed that checkpoint yet
						reachedCheckpointWhichHasntBeenPassed = true;
					} else {
						//claims that it has passed that checkpoint
						if (reachedCheckpointWhichHasntBeenPassed == true) {
							//but hasn't passed one of the checkpoints before it, therefore it couldn't have actually passed the checkpoint
							checkpointLaps[lapIndex].hasPassed[checkIndex] = false;
						}
					}
					checkIndex = checkIndex + 1;
				}
				lapIndex = lapIndex + 1;
			}

			//find the limiting position number as a public float
			int lapNum = posScript.currentLapNumber - posScript.lapsGoneBack; //Get the lap number from the RPS_Position script
			bool hasSet = false;
			//Go through the array of laps
			foreach (checkpointLap checkLap in checkpointLaps) {
				if (checkLap.lapNumber == lapNum) { //If it is the current lap
					hasSet = true;
					int checkLimitIndex = 0;
					float tempLimitedPos = Mathf.Infinity;
					bool hasSetLimit = false;
					foreach (bool hasPassed in checkLap.hasPassed) { //Go through the current lap's checkpoints
						if (hasPassed == false) {
							if (checkpoints[checkLimitIndex].limitNumber < tempLimitedPos) {
								tempLimitedPos = checkpoints[checkLimitIndex].limitNumber; //Find the limited position number
								hasSetLimit = true;
							}
						}
						checkLimitIndex = checkLimitIndex + 1;
					}
					if (hasSetLimit == true) {
						limitedPosition = tempLimitedPos;
					} else {
						limitedPosition = Mathf.Infinity;
					}

				}
			}
			if (hasSet == false) {
				limitedPosition = Mathf.Infinity;
			}
		}
	}

	//This function is called by the RPS_Position script when using checkpoints and the object completes a lap
	public void checkpointsForwardLap () {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
		} else {
			//Need to add the new laps checkpoints to the checkpointLaps array unless it already contains them
			int currentLapNum = posScript.currentLapNumber - posScript.lapsGoneBack;
			//check the laps checkpoints are not already recorded
			bool lapNumberFound = false;
			foreach (checkpointLap checkLap in checkpointLaps) {
				if (checkLap.lapNumber == currentLapNum) {
					lapNumberFound = true;
				}
			}
			if (lapNumberFound == false) { //Lap number's checkpoints are not being recorded, so need to start recording them
				//Add them to the checkpointLap array
				//As it is a new lap going forward, none of the checkpoints on that lap have been passed yet
				checkpointLap newLap = new checkpointLap();
				newLap.lapNumber = currentLapNum;
				List<bool> newLapsCheckpointsPassed = new List<bool>();
				foreach (checkpoint point in checkpoints) {
					if (point != null) {
						newLapsCheckpointsPassed.Add(false);
					}
				}
				newLap.hasPassed = newLapsCheckpointsPassed;
				checkpointLaps.Add(newLap); //Add the new laps checkpoints to the array
			}
		}
	}

	//This function is called by the RPS_Position script when an object goes back a lap
	public void checkpointsBackLap () {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
		} else {
			//Need to add the new laps checkpoints to the checkpointLaps array unless it already contains them
			int currentLapNum = posScript.currentLapNumber - posScript.lapsGoneBack;
			//check the laps checkpoints are not already recorded
			bool lapNumberFound = false;
			foreach (checkpointLap checkLap in checkpointLaps) {
				if (checkLap.lapNumber == currentLapNum) {
					lapNumberFound = true;
				}
			}
			if (lapNumberFound == false) { //Lap number's checkpoints are not being recorded, so need to start recording them
				//Add them to the checkpointLap array
				//As it has gone back a lap, all of the checkpoints on that lap have already been passed (as they don't need to be passed)
				checkpointLap newLap = new checkpointLap();
				newLap.lapNumber = currentLapNum;
				List<bool> newLapsCheckpointsPassed = new List<bool>();
				foreach (checkpoint point in checkpoints) {
					if (point != null) {
						newLapsCheckpointsPassed.Add(true);
					}
				}
				newLap.hasPassed = newLapsCheckpointsPassed;
				checkpointLaps.Insert(0, newLap);
			}
		}
	}

	//This function is called by the RPS_Position script when going forward a lap to check all of the checkpoints on the lap have been passed
	public bool CheckCanGoForwardLap () {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
			return false;
		} else {
			int lapNum = posScript.currentLapNumber - posScript.lapsGoneBack; //Gets the current lap number from the RPS_Position script
			bool canGoForward = true;
			foreach (checkpointLap checkLap in checkpointLaps) {
				if (checkLap.lapNumber == lapNum) {
					foreach (bool check in checkLap.hasPassed) { //Goes through all of the lap's checkpoints
						if (check == false) { //makes sure they have all been passed
							canGoForward = false;
						}
					}
				}
			}
			return canGoForward; //returns a bool for if the object can go forward a lap or not
		}
	}

	//This function must be called by one of your scripts when a checkpoint is passed. In the demo scenes we have used a triggers on each checkpoint to detect this.
	//Look at the demo scenes or pdf for more information on how you should do this.
	public void CheckpointPassed (int checkpointNumber) {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
		} else {
			int lapNum = posScript.currentLapNumber - posScript.lapsGoneBack;
			bool lastBool = true;
			bool hasReachedFalse = false;
			int lapindex = 0;
			foreach (checkpointLap checkLap in checkpointLaps) {
				int checkindex = 0;
				foreach (bool checkPassed in checkLap.hasPassed) { //Iterates through every checkpoint to make sure no checkpoints before it have been missed
					if (hasReachedFalse == true) {
						checkpointLaps[lapindex].hasPassed[checkindex] = false;
					}
					if ((lastBool == true)&&(checkPassed == false)) {
						if ((checkLap.lapNumber == lapNum)&&(checkpointNumber == checkindex)) {
							if (hasReachedFalse == false) {
								checkpointLaps[lapindex].hasPassed[checkindex] = true; //Sets the checkpoint has been passed to true in the array
								hasReachedFalse = true;
							}
						}
					}
					lastBool = checkPassed;
					checkindex = checkindex + 1;
					if (checkPassed == false) {
						hasReachedFalse = true;
					}
				}
				lapindex = lapindex + 1;
			}
		}
	}

	//Extra function which returns the number of the next checkpoint. Returns -1 if there isn't a new one
	public int nextCheckpointNumber () {
		int checkNum = -1;
		bool hasfound = false;
		foreach (checkpointLap lap in checkpointLaps) {
			int eachCheckNum = 0;
			foreach (bool passed in lap.hasPassed) {
				if ((passed == false)&&(hasfound == false)) {
					hasfound = true;
					checkNum = eachCheckNum;
				}
				eachCheckNum = eachCheckNum + 1;
			}
		}
		if (hasfound == true) {
			return checkNum;
		} else {
			return -1;
		}
	}

	//Extra function which returns the number of the last checkpoint. Returns -1 if there isn't one
	public int lastCheckpointNumber () {
		int checkNum = -1;
		bool hasfound = false;
		foreach (checkpointLap lap in checkpointLaps) {
			int eachCheckNum = 0;
			foreach (bool passed in lap.hasPassed) {
				if (passed == true) {
					hasfound = true;
					checkNum = eachCheckNum;
				}
				eachCheckNum = eachCheckNum + 1;
			}
		}
		if (hasfound == true) {
			return checkNum;
		} else {
			return -1;
		}
	}

	//Extra function which returns if a checkpoint has been missed or not
	public bool checkpointMissed () {
		if (posScript == null) {
			posScript = gameObject.GetComponent<RPS_Position>(); //Find the RPS_Position script on this object and store as variable
		}
		if (posScript == null) { //Return an error if there is no RPS_Position script attached
			Debug.Log ("RPS Error: all RPS_Checkpoints scripts should be attached to an object which has a RPS_Position script");
			return false;
		} else {
			if (limitedPosition < posScript.currentNearestPositionUnlimited) {
				return true;
			} else {
				return false;
			}
		}
	}

	//Extra function which returns the checkpoint which has been missed. Returns -1 if checkpoint hasn't been missed.
	public int checkpointNumberMissed () {
		if (checkpointMissed() == false) {
			//checkpoint has not been missed
			return -1;
		} else {
			//checkpoint has been missed
			return nextCheckpointNumber();
		}
	}
}
