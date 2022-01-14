using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inGame_UIManager : MonoBehaviour {

	//Public variables set on the prefab
	public GameObject ui_GameOver;
	public GameObject[] boosterPacks;
	public Texture texture_boosterEmpty;
	public Texture texture_boosterFull;
	public Text text_Timer;
	public Text text_Level;

	//Public debug values
	public GameObject debugPanel;
	public bool debugActive = false;
	public GameObject button_Submit;
	public GameObject button_ResetBoosters;
	public GameObject text_Thrust;
	public GameObject text_Break;
	public GameObject text_Torque;
	public GameObject text_Booster;
	public GameObject phText_Thrust;
	public GameObject phText_Break;
	public GameObject phText_Torque;
	public GameObject phText_Booster;

	private bool levelComplete = false;
	private string time_String;
	private int maxBoosters;
	private int boostersAvailable;
	private int boosterIndex = 0;
	private float gameTime;
	private float startTime;

	// Use this for initialization
	void Start ()
	{
		globalOptions.uiManager = this.gameObject;
		startTime = Time.time;
		text_Level.text = "Level: " + globalOptions.gameLevel;
		maxBoosters = boosterPacks.Length;
		boostersAvailable = maxBoosters;
	}

	void Update()
	{
		if (levelComplete == false)
		{
			gameTime = Time.time - startTime;
			string minutes = ((int)gameTime / 60).ToString ();
			string seconds = (gameTime % 60).ToString ("f2");
			time_String = minutes + ":" + seconds;
			text_Timer.text = "Time: " + time_String;
		}
		if (Input.GetKeyUp(KeyCode.F4))
		{
			DebugMenuOpen ();
		}
	}

	public void GameOverScreen()
	{
		ui_GameOver.GetComponent<Animator> ().SetBool ("gameOverEnabled", true);
	}

	public void UpdateLevel()
	{
		
	}

	public void UpdateTime()
	{
		globalOptions.levelTime_String = time_String;
		globalOptions.levelTime = gameTime;
	}

	public void UpdateFuel()
	{
		
	}

	public void CompleteLevel()
	{
		levelComplete = true;
		UpdateFuel ();
		UpdateTime ();
		UpdateLevel ();
	}

	//Rturns the amount of boosters
	public int CheckBoosterCount()
	{
		return boostersAvailable;
	}

	public void SpendBooster()
	{
		boostersAvailable--;
		boosterPacks [boosterIndex].GetComponent<RawImage> ().texture = texture_boosterEmpty;
		boosterIndex++;
	}

	public void ResetBoosters()
	{
		boosterIndex = 0;
		boostersAvailable = maxBoosters;
		foreach (GameObject boosterPack in boosterPacks)
		{
			boosterPack.GetComponent<RawImage> ().texture = texture_boosterFull;
		}
	}

	public void ResetFuel()
	{
		globalOptions.playerFuelGauge.GetComponent<fuelGauge> ().fuelLevel = globalOptions.playerFuelGauge.GetComponent<fuelGauge> ().fuelMax;
	}

	public void DebugMenuOpen()
	{
		debugActive = !debugActive;
		debugPanel.SetActive (debugActive);
		if (debugActive == true)
		{
			phText_Thrust.GetComponent<Text> ().text = globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Thrust.ToString ();
			phText_Break.GetComponent<Text> ().text = globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Break.ToString ();
			phText_Torque.GetComponent<Text> ().text = globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Torque.ToString ();
			phText_Booster.GetComponent<Text> ().text = globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Boost.ToString ();
		}
	}

	public void DebugSubmitChanges()
	{
		globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Thrust = float.Parse (text_Thrust.GetComponent<Text> ().text);
		globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Break = float.Parse (text_Break.GetComponent<Text> ().text);
		globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Torque = float.Parse (text_Torque.GetComponent<Text> ().text);
		globalOptions.playerShip.GetComponent<playerShip_Control> ().player_Boost = float.Parse (text_Booster.GetComponent<Text> ().text);
	}
}