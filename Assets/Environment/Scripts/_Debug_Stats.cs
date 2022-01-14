using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Debug_Stats : MonoBehaviour {

	public int astCount;
	public int astChunkCount;
	public int blkHoleCount;
	public int sptnkCount;
	public int hzrdCount;
	
	// Update is called once per frame
	void Update ()
	{
		astCount = globalOptions.asteroidCount;
		astChunkCount = globalOptions.asteroidChunkCount;
		blkHoleCount = globalOptions.blackHoleCount;
		sptnkCount = globalOptions.spaceJunkCount;
		hzrdCount = globalOptions.hazardCount;
	}
}
