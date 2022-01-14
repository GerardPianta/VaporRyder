using System.Collections;
using UnityEngine;

public class globalOptions : MonoBehaviour
{
	public static globalOptions instance;

	public static bool debugEnabled;

	//Global Options
	public static int resolutionLevel = 1;
	// the resolutionLevel variable controls the in game resolution which is referenced in renderTextureChanger.cs
	public static float volumeLevel;
	public static GameObject playerShip;
	public static GameObject playerFuelGauge;
	public static GameObject warpPoint;
	public static GameObject gameBoard;
	public static GameObject uiManager;

	//Gameplay Variables
	public static int gameLevel = 1;
	//These are set by environment_Behavior usiong the sphere collider's radius of the GameBoard asset it's attached to
	public static float gameBoard_Radius;
	public static float gameBoard_InnerRadius;
	public static float gameBoard_OuterRadius;
	public static float asteroid_MinSpeed = -3;
	public static float asteroid_MaxSpeed = 3;
	public static float asteroid_MinSize = 15;
	public static float asteroid_MaxSize = 40;
	//this variable controls the max speed the hazards can attain from black holes
	public static float maxSpeedFromBlackHole = 15;

	//These variables are based on the gameLevel and increase for difficulty
	int asteroid_BaseValue = 40; //old 60
	public static int minAsteroids;
	public static int maxAsteroids;
	int asteroidChunk_BaseValue;
	public static int maxAsteroidChunks;
	int spaceJunk_BaseValue = 25;
	public static int minSpaceJunk;
	public static int maxSpaceJunk;
	int blackHole_BaseValue = 3;
	public static int minBlackHoles;
	public static int maxBlackHoles;
	int hazard_BaseValue;
	public static int maxHazards;
	public static float exitDistance_BaseValue = 30; 
	public static float exitDistance;

	//Variables for tracking the number of hazards on screen
	public static int asteroidCount;
	public static int asteroidChunkCount;
	public static int spaceJunkCount;
	public static int blackHoleCount;
	public static int hazardCount;

	public static string levelTime_String;
	public static float levelTime;
	public static float fuelLeft;

	//Notes
	//Add global speed modifier function and variable to subtly increase the speed based oin the game level.
	//make a global level radius to base black hole random position, asteroid spawn, and warp point starting position on
	//make strength of gravity controlled by gameLevel


	void Awake()
	{
		ResetStatics ();
		//this.InstantiateGlobals ();
	}

	void Update ()
	{
		hazardCount = asteroidCount + asteroidChunkCount + spaceJunkCount + blackHoleCount;
	}

	void ResetStatics()
	{
		asteroidCount = 0;
		asteroidChunkCount = 0;
		spaceJunkCount = 0;
		blackHoleCount = 0;
	}

	public static void ResetGame()
	{
		gameLevel = 1;
		//Add ciode to reset the UI
	}

	// Use this for initialization
	void Start ()
	{
		volumeLevel = 1.0f;
		LevelDefaults ();
	}

	private void InstantiateGlobals()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad (gameObject);
		}
		else if (instance != this)
		{
			Destroy (this.gameObject);
		}
	}

	public void LevelDefaults()
	{
		asteroidChunk_BaseValue = asteroid_BaseValue * 3;
		hazard_BaseValue = (asteroid_BaseValue + asteroidChunk_BaseValue + spaceJunk_BaseValue + blackHole_BaseValue);
		minAsteroids = asteroid_BaseValue + ((gameLevel - 1) * 5);
		maxAsteroids = minAsteroids + 25;
		maxAsteroidChunks = maxAsteroids * 3;
		minSpaceJunk = spaceJunk_BaseValue + ((gameLevel - 1) * 3);
		maxSpaceJunk = minSpaceJunk + 15;
		minBlackHoles = blackHole_BaseValue + Mathf.RoundToInt((gameLevel - 1) / 1);
		maxBlackHoles = blackHole_BaseValue + Mathf.RoundToInt((gameLevel - 1) / 1);
		maxHazards = (minAsteroids + ((maxAsteroids-minAsteroids)/2)) + maxAsteroidChunks + (minSpaceJunk + ((maxSpaceJunk-minSpaceJunk)/2)) + maxBlackHoles;
		exitDistance = exitDistance_BaseValue + ((gameLevel - 1) * 25);
		StartCoroutine ("SetGameBoard");
	}

	IEnumerator SetGameBoard()
	{
		yield return new WaitForSeconds (0.01f);
		gameBoard.GetComponent<environment_Behavior> ().PopulateGameBoard ();
	}

	public static void UpdateVolume()
	{
		AudioListener.volume = volumeLevel;
	}
}
