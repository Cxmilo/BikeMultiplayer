/*
Race Positioning System by Solution Studios

Script Name: RPS_3D_RestartButton.cs

Description:
Used in the 3D demo scene.
Basic script restarting the scene when the restart buttons are pressed.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RPS_3D_RestartButton : MonoBehaviour {

	// Called when a restart button is pressed
	public void Restart () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
