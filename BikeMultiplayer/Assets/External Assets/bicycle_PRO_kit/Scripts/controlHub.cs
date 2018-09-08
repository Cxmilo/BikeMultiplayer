// Writen by Boris Chuprin smokerr@mail.ru
using UnityEngine;
using System.Collections;

//this is script contains all variables translated to bike's script.
//so, it's just a holder for all control variables
//mobile/keyboard scripts sends nums(float, int, bools) to this one

public class controlHub : MonoBehaviour {//need that for mobile controls
    [System.Serializable]
    public struct MapController
    {
        public float Vertical;//variable translated to bike script for bike accelerate/stop and leaning
        public float Horizontal;//variable translated to bike script for pilot's mass shift

        public float VerticalMassShift;//variable for pilot's mass translate along bike
        public float HorizontalMassShift;//variable for pilot's mass translate across bike

        public bool rearBrakeOn;//this variable says to bike's script to use rear brake
        public bool restartBike;//this variable says to bike's script restart
        public bool fullRestartBike; //this variable says to bike's script to full restart

        public bool reverse;//for reverse speed
    }

    public MapController player_1;
    public MapController player_2;
    public MapController player_3;

    public MapController GetPlayerController(int playerIndex)
    {
        
        switch (playerIndex)
        {
            default:
            case 0:
                return player_1;
            case 1:
                return player_2;
            case 2:
                return player_3;
        }
    }


}
