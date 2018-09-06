/*
Race Positioning System by Solution Studios

Script Name: RPS_Sprint1Demo_Finished.cs

Description:
Used in the Sprint 1 demo scene to detect when the race has finished and show the appropriate UI.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RPS_Sprint1Demo_Finished : MonoBehaviour {

    public RPS_Position playerCarPositionScript; //The RPS_Position script on the player car
    int finishedPostion = 0;
    public GameObject finishedUI; //Object which is parent of main finished UI
    public GameObject came1stUI; //Object for finished 1st UI
    public GameObject came2ndUI; //Object for finished 2nd UI
    public GameObject came3rdUI; //Object for finsihed 3rd UI

    public void RaceFinished () //Called by the RPS_Sprint1Demo_CheckpointCollider script when the car hits the finish line trigger
    {
        finishedPostion = playerCarPositionScript.currentRacePosition; //Gets the race position
        finishedUI.gameObject.SetActive(true); //Shows main finished UI
        if (finishedPostion == 0) //If the car finished 1st
        {
            came1stUI.gameObject.SetActive(true); //Show 1st UI
        }
        if (finishedPostion == 1) //If the car finished 2nd
        {
            came2ndUI.gameObject.SetActive(true); //Show 2nd UI
        }
        if (finishedPostion == 2) //If the car finished 3rd
        {
            came3rdUI.gameObject.SetActive(true); //Show 3rd UI
        }
        Time.timeScale = 0.0f; //Freeze the race scene
    }

	public void RestartButtonPressed () //Called when  the restart button is pressed
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the level
    }
}
