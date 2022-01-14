using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environment_Behavior : MonoBehaviour {

	//Public variables set on the prefab
	public GameObject[] asteroidChunks;
	public GameObject[] asteroids;
	public GameObject[] spaceJunk;
	public GameObject blackHole;
	public GameObject warpPoint;
	public GameObject planet;
	public int minPlanets = 125;
	public int maxPlanets = 200;

	private bool gameBoardPopulated = false;


	// Use this for initialization
	void Start ()
	{
		globalOptions.gameBoard_Radius = this.GetComponent<SphereCollider>().radius;
		globalOptions.gameBoard_InnerRadius = globalOptions.gameBoard_Radius - 20;
		globalOptions.gameBoard_OuterRadius = globalOptions.gameBoard_Radius - 5;;
		globalOptions.gameBoard = gameObject;
		//PopulateGameBoard ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (globalOptions.maxHazards);
		if (gameBoardPopulated)
		{
			if (globalOptions.hazardCount < globalOptions.maxHazards)
			{
				if (globalOptions.asteroidCount < globalOptions.minAsteroids)
				{
					SpawnAsteroid ();
				}

				if (globalOptions.spaceJunkCount < globalOptions.minSpaceJunk)
				{
					SpawnSpaceJunk ();
				}
			}

			if (globalOptions.blackHoleCount < globalOptions.minBlackHoles)
			{
				SpawnBlackHole ();
			}
		}
	}

	void OnTriggerExit(Collider hazardToDestroy)
	{
		//Bug: Null reference errors on sputnik's energy wave game object as it doesn't contain a destroy self script, but it's parent does, so this should not afdfect anything.
		hazardToDestroy.gameObject.GetComponent<hazard_DestroySelf> ().DestroySelf ();
	}

	public void PopulateGameBoard()
	{
		int index;
		int arrayLength = (asteroids.Length - 1);
		Vector3 position;
		for (int i = 0; i < globalOptions.minAsteroids; i++)
		{
			index = Random.Range (0, arrayLength);
			position = Random.insideUnitCircle.normalized * Random.Range (30, globalOptions.gameBoard_OuterRadius);
			GameObject hazard_asteroid = Instantiate (asteroids [index], new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
		}
		arrayLength = (spaceJunk.Length - 1);
		for (int i = 0; i < globalOptions.minSpaceJunk; i++)
		{
			index = Random.Range (0, arrayLength);
			position = Random.insideUnitCircle.normalized * Random.Range (30, globalOptions.gameBoard_OuterRadius);
			GameObject hazard_spaceJunk = Instantiate (spaceJunk [index], new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
			hazard_spaceJunk.GetComponent<AudioSource> ().Play();
		}
		for (int i = 0; i < globalOptions.maxBlackHoles; i++)
		{
			position = Random.insideUnitCircle.normalized * Random.Range (40, (globalOptions.gameBoard_InnerRadius/3));
			GameObject hazard_blackHole = Instantiate (blackHole, new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
			hazard_blackHole.GetComponent<AudioSource> ().Play();
		}
		int randPlanets = Random.Range (minPlanets, maxPlanets);
		for (int i = 0; i < randPlanets; i++)
		{
			position = Random.insideUnitCircle * globalOptions.gameBoard_OuterRadius;
			float randHeight = Random.Range(-15, -100);
			float randScale = Random.Range (0.5f, 4.5f);
			GameObject env_planet = Instantiate (planet, new Vector3 (position.x, randHeight, position.y), Quaternion.identity) as GameObject;
			env_planet.transform.localScale = new Vector3 (randScale, randScale, randScale);
		}
		position = Random.insideUnitCircle.normalized * Random.Range (50, globalOptions.exitDistance);
		GameObject env_warpPoint = Instantiate (warpPoint, new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
		gameBoardPopulated = true;
	}

	void SpawnAsteroid ()
	{
		int arrayLength = (asteroids.Length - 1);
		int index = Random.Range (0, arrayLength);
		Vector3 position = Random.insideUnitCircle.normalized * Random.Range (globalOptions.gameBoard_InnerRadius, globalOptions.gameBoard_OuterRadius);
		GameObject hazard_asteroid = Instantiate (asteroids [index], new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
	}

	void SpawnAsteroidChunk(Vector3 position, Vector3 scale)
	{
		int arrayLength = (asteroidChunks.Length - 1);
		int index = Random.Range (0, arrayLength);
		GameObject hazard_asteroidChunk = Instantiate (asteroidChunks[index], position, Quaternion.identity) as GameObject;
		hazard_asteroidChunk.transform.localScale = scale;
	}

	void SpawnSpaceJunk()
	{
		int arrayLength = (spaceJunk.Length - 1);
		int index = Random.Range (0, arrayLength);
		Vector3 position = Random.insideUnitCircle.normalized * Random.Range (globalOptions.gameBoard_InnerRadius, globalOptions.gameBoard_OuterRadius);
		GameObject hazard_spaceJunk = Instantiate (spaceJunk [index], new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
		hazard_spaceJunk.GetComponent<AudioSource> ().Play();
	}

	void SpawnBlackHole()
	{
		Vector3 position = Random.insideUnitCircle.normalized * Random.Range (globalOptions.gameBoard_InnerRadius, globalOptions.gameBoard_OuterRadius);
		GameObject hazard_blackHole = Instantiate (blackHole, new Vector3 (position.x, 0, position.y), Quaternion.identity) as GameObject;
		hazard_blackHole.GetComponent<AudioSource> ().Play();
	}
}
