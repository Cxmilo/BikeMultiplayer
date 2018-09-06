/*
Race Positioning System by Solution Studios

Script Name: RPS_Inspector.cs

Description:
This script just stores variables for each object involved in a race, allowing the use of some editor features.
It has no effect at runtime.
*/

using UnityEngine;
using System.Collections;

public class RPS_Inspector : MonoBehaviour {

    //which part of the Inpector UI to show
	public int part = 0;
	public int insidePart = 0;
    
    //Useful objects and scripts being stored for editor use
	public RPS_Position posScript;
	public GameObject storageObj;
	public RPS_Storage storageScript;

    //Temporary add RPS_ScreenUI script variables (Image)
	public UnityEngine.UI.Image screenUIImage;
	public Sprite[] screenUISprites;
	public bool spriteElementsExpand = true;
	public int spriteElementsSize = 1;

    //Temporary add RPS_ScreenUI script variables (Text)
    public UnityEngine.UI.Text screenUIText;
	public bool screenUITextShowEndings = true;

    //Temporary add RPS_LegacyScreenUI script variables (Texture)
    public GUITexture legacyScreenUITexture;
	public Texture[] legacyScreenUIRaceTextures;
	public bool legacyScreenUIElementsExpand = true;
	public int legacyScreenUIElementsSize = 1;

    //Temporary add RPS_LegacyScreenUI script variables (Text)
    public GUIText legacyScreenUIText;
	public bool legacyScreenUITextShowEnding = true;

    //Temporary add RPS_PositionTexture script variables
    public Renderer posTexturePlaneRenderer;
	public Texture[] posTextureRaceTextures;
	public bool posTextureElementsExpand = true;
	public int posTextureElementsSize = 1;

    //Temporary add RPS_Lap script variables
    public bool lapHasEnd = true;
	public bool lapFreezePos = true;
	public int lapFirstLap = 1;
	public int lapLastLap = 3;

    //Temporary add RPS_LapUI script variables
    public UnityEngine.UI.Text lapUIText;
	public bool lapUIShowTotalLaps = true;
	public bool lapUIchangewhenfinished = true;
	public string lapchangeToText = "Finished";
	public bool lapchangeFontSize = false;
	public int lapnewFontSize = 0;

    //Temporary add RPS_LegacyLapUI script variables
    public GUIText llapUIText;
	public bool llapUIShowTotalLaps = true;
	public bool llapUIchangewhenfinished = true;
	public string llapchangeToText = "Finished";
	public bool llapchangeFontSize = false;
	public int llapnewFontSize = 0;

}
