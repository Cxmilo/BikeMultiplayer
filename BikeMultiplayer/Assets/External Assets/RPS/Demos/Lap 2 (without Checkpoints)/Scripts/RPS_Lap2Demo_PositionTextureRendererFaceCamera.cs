/*
Race Positioning System by Solution Studios

Script Name: RPS_Lap2Demo_PositionTextureRendererFaceCamera.cs

Description:
Used in the lap 2 demo scene, attached to the objects which show the AI positions above the AI objects.
Make the position of the AI object face the camera.
*/

using UnityEngine;
using System.Collections;

public class RPS_Lap2Demo_PositionTextureRendererFaceCamera : MonoBehaviour {

	//The Camera Object
	public Transform cameraObj;

	void Update () {
		//Face the camera
		transform.LookAt(cameraObj.position);
	}
}
