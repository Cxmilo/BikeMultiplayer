/*
Race Positioning System by Solution Studios

Script Name: RPS_PS3D.cs

Description:
This is a minor script and can be ignored.
This script is used by the editor at the green cubes stage when setting up Position Sensors.
Its applied to cubes to remember which cubes are from which PositionSensor number in 3 dimensions.
The script is removed after this stage.
We could have just used parents of the green cubes to remember the PositionSensor numbers they are from, but this would have required a completley different solution to be programmed for 2 dimensional and 3 dimensional PositionSensors.
*/

using UnityEngine;
using System.Collections;

public class RPS_PS3D : MonoBehaviour {

	public int setNumber = 0;

}
