/*
Race Positioning System by Solution Studios

Script Name: RPS_3D_CheckpointNumber.cs

Description:
Used in the Sprint 1 demo scene, attached to the collider of each checkpoint.
This script is accessed by the RPS_Sprint1Demo_CheckpointCollider script when the player car hits a checkpoint.
This script tells the player car which checkpoint it has hit so it can tell the RPS_Checkpoints script.
*/

using UnityEngine;
using System.Collections;

public class RPS_Sprint1Demo_CheckpointNumber : MonoBehaviour {

    public int checkpointNumber = 0; //The checkpoints number

}
