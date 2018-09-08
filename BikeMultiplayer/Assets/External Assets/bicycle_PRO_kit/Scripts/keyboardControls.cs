// Writen by Boris Chuprin smokerr@mail.ru
using UnityEngine;
using System.Collections;

public class keyboardControls : MonoBehaviour {



	private GameObject ctrlHub;// making a link to corresponding bike's script
	private controlHub outsideControls;// making a link to corresponding bike's script
	


	// Use this for initialization
	void Start () {
		ctrlHub = GameObject.Find("gameScenario");//link to GameObject with script "controlHub"
		outsideControls = ctrlHub.GetComponent<controlHub>();// making a link to corresponding bike's script
	}
	
	// Update is called once per frame
	void Update () {
		//////////////////////////////////// ACCELERATE, braking & 'full throttle - manual trick' //////////////////////////////////////////////
		//Alpha2 is key "2". Used to make manual. Also, it can be achived by 100% "throtle on mobile joystick"
		if (!Input.GetKey (KeyCode.Alpha2)) {
			outsideControls.player_1.Vertical = Input.GetAxis ("Vertical") / 1.112f;//to get less than 0.9 as acceleration to prevent wheelie(wheelie begins at >0.9)
			if(Input.GetAxis ("Vertical") <0) outsideControls.player_1.Vertical = outsideControls.player_1.Vertical * 1.112f;//need to get 1(full power) for front brake

            outsideControls.player_2.Vertical = Input.GetAxis("Vertical_2") / 1.112f;//to get less than 0.9 as acceleration to prevent wheelie(wheelie begins at >0.9)
            if (Input.GetAxis("Vertical_2") < 0) outsideControls.player_2.Vertical = outsideControls.player_2.Vertical * 1.112f;//need to get 1(full power) for front brake

            outsideControls.player_3.Vertical = Input.GetAxis("Vertical_3") / 1.112f;//to get less than 0.9 as acceleration to prevent wheelie(wheelie begins at >0.9)
            if (Input.GetAxis("Vertical_3") < 0) outsideControls.player_3.Vertical = outsideControls.player_3.Vertical * 1.112f;//need to get 1(full power) for front brake

        }

		//////////////////////////////////// STEERING /////////////////////////////////////////////////////////////////////////
		outsideControls.player_1.Horizontal = Input.GetAxis("Horizontal");
		if (Input.GetKey (KeyCode.Alpha2)) outsideControls.player_1.Vertical = 1;

        outsideControls.player_2.Horizontal = Input.GetAxis("Horizontal_2");
        if (Input.GetKey(KeyCode.Alpha2)) outsideControls.player_2.Vertical = 1;

        outsideControls.player_3.Horizontal = Input.GetAxis("Horizontal_3");
        if (Input.GetKey(KeyCode.Alpha2)) outsideControls.player_3.Vertical = 1;


        //}

        //////////////////////////////////// Rider's mass translate ////////////////////////////////////////////////////////////
        //this strings controls pilot's mass shift along bike(vertical)
        if (Input.GetKey (KeyCode.F)) {
			outsideControls.player_1.VerticalMassShift = outsideControls.player_1.VerticalMassShift += 0.1f;
			if (outsideControls.player_1.VerticalMassShift > 1.0f) outsideControls.player_1.VerticalMassShift = 1.0f;
		}

		if (Input.GetKey(KeyCode.V)){
			outsideControls.player_1.VerticalMassShift = outsideControls.player_1.VerticalMassShift -= 0.1f;
			if (outsideControls.player_1.VerticalMassShift < -1.0f) outsideControls.player_1.VerticalMassShift = -1.0f;
		}
		if(!Input.GetKey(KeyCode.F) && !Input.GetKey(KeyCode.V)) outsideControls.player_1.VerticalMassShift = 0;

		//this strings controls pilot's mass shift across bike(horizontal)
		if (Input.GetKey(KeyCode.E)){
			outsideControls.player_1.HorizontalMassShift = outsideControls.player_1.HorizontalMassShift += 0.1f;
			if (outsideControls.player_1.HorizontalMassShift >1.0f) outsideControls.player_1.HorizontalMassShift = 1.0f;
		}

		if (Input.GetKey(KeyCode.Q)){
			outsideControls.player_1.HorizontalMassShift = outsideControls.player_1.HorizontalMassShift -= 0.1f;
			if (outsideControls.player_1.HorizontalMassShift < -1.0f) outsideControls.player_1.HorizontalMassShift = -1.0f;
		}
		if(!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q)) outsideControls.player_1.HorizontalMassShift = 0;


		//////////////////////////////////// Rear Brake ////////////////////////////////////////////////////////////////
		// Rear Brake
		if (Input.GetKey (KeyCode.X)) {
			outsideControls.player_1.rearBrakeOn = true;
		} else
			outsideControls.player_1.rearBrakeOn = false;

		//////////////////////////////////// Restart ////////////////////////////////////////////////////////////////
		// Restart & full restart
		if (Input.GetKey (KeyCode.R)) {
			outsideControls.player_1.restartBike = true;
		} else
			outsideControls.player_1.restartBike = false;

		// RightShift for full restart
		if (Input.GetKey (KeyCode.RightShift)) {
			outsideControls.player_1.fullRestartBike = true;
		} else
			outsideControls.player_1.fullRestartBike = false;

		//////////////////////////////////// Reverse ////////////////////////////////////////////////////////////////
		// Restart & full restart
		if(Input.GetKeyDown(KeyCode.C)){
				outsideControls.player_1.reverse = true;
		} else outsideControls.player_1.reverse = false;
		///
	}
}
