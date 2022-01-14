using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazard_DestroySelf : MonoBehaviour {

	public enum HazardType
	{
		Asteroid,
		AsteroidChunk,
		SpaceJunk,
		BlackHole
	}

	public HazardType hazardType;


	// Use this for initialization
	void Start ()
	{
		
	}

	public void DestroySelf()
	{
		switch (hazardType)
		{
		case HazardType.Asteroid:
			globalOptions.asteroidCount--;
			break;
		case HazardType.AsteroidChunk:
			globalOptions.asteroidChunkCount--;
			break;
		case HazardType.SpaceJunk:
			globalOptions.spaceJunkCount--;
			break;
		case HazardType.BlackHole:
			globalOptions.blackHoleCount--;
			break;
		default:
			Debug.LogError("hazardType has not been set on " + gameObject + ", check the prefab to make sure the hazardType has been set.");
			break;
		}
		Destroy(this.gameObject);
	}
}