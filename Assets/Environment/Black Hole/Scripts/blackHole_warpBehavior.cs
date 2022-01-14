using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole_warpBehavior : MonoBehaviour {

	//Public variables set on the prefab
	public GameObject player;
	public float minDistance = 25;
	public float maxDistance = 75;
	public float minCoolDown;
	public float maxCoolDown;
	public float blackHoleWarpTime;
	public float blackHoleCoolDownTime;
	public AudioClip audio_warpIn;
	public AudioClip audio_warpOut;

	bool blackHole_Warping = false;

	// Use this for initialization
	void Start ()
	{
		
	}

	void FixedUpdate()
	{
		Collider[] hitObjects = Physics.OverlapSphere (gameObject.transform.position, 25);
		foreach (Collider hitObject in hitObjects)
		{
			if (hitObject.gameObject.CompareTag ("warpPoint") && blackHole_Warping == false)
			{
				StartCoroutine ("BlackHoleDisappear");
				break;
			}
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			StartCoroutine ("BlackHoleDisappear");
		}
	}

	public Vector3 GetPosition(GameObject go)
	{
		return go.transform.position;
	}

	public IEnumerator BlackHoleDisappear ()
	{
		blackHole_Warping = true;
		gameObject.GetComponent<Animator> ().SetBool ("warpOut", true);
		soundManager.PlaySound (audio_warpOut, GetPosition(this.gameObject), 1);
		blackHoleCoolDownTime = Random.Range (minCoolDown, maxCoolDown);
		gameObject.GetComponent<blackHole_Behavior> ().isActive = false;
		yield return new WaitForSeconds (blackHoleCoolDownTime);
		StartCoroutine ("BlackHoleAppear");
	}


	//gets a random position within a certain distance from the player, but not close to the player
	IEnumerator BlackHoleAppear()
	{
		gameObject.GetComponent<blackHole_Behavior> ().isActive = true;
		Vector3 playerPosition = GetPosition(globalOptions.playerShip);
		float ranXDist = Random.Range(minDistance, maxDistance);
		float ranZDist = Random.Range(minDistance, maxDistance);
		float ranDirX = Random.Range(-1,1);
		float ranDirZ = Random.Range(-1,1);
		float ranX = 0;
		float ranZ = 0;
		if(ranDirX >= 0)
			ranX = ranXDist + playerPosition.x;
		if(ranDirX < 0)
			ranX = (ranXDist*-1) + playerPosition.x;
		if(ranDirZ >= 0)
			ranZ = ranZDist + playerPosition.z;
		if(ranDirZ < 0)
			ranZ = (ranZDist*-1) + playerPosition.z;
		gameObject.transform.position = new Vector3(ranX, 0, ranZ);
		soundManager.PlaySound (audio_warpIn, GetPosition(this.gameObject), 1);
		gameObject.GetComponent<Animator> ().SetBool ("warpIn", true);
		yield return new WaitForSeconds (blackHoleWarpTime);
		blackHole_Warping = false;
		gameObject.GetComponent<Animator> ().SetBool ("warpOut", false);
		gameObject.GetComponent<Animator> ().SetBool ("warpIn", false);
	}
}
