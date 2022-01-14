using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelComplete_UIManager : MonoBehaviour {

	public Text text_Congrats;
	public Text text_time;
	public Text text_fuel;

	void Start ()
	{
		text_Congrats.text = "Congratulations! You beat level " + globalOptions.gameLevel.ToString();
		text_time.text = "Time: " + globalOptions.levelTime_String;
		text_fuel.text = "Fuel saved: " + globalOptions.fuelLeft.ToString ("f3");
	}
}