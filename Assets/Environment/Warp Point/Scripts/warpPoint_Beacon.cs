using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpPoint_Beacon : MonoBehaviour {

	private AudioSource audioSource;
	public AudioClip beaconSound;
	public float beaconMaxDistance = 50.0f;
	public float beaconMinDistance = 2.5f;
	public float beaconTime = 3.0f;

	public GameObject beaconLight;
	private GameObject player;

	// Use this for initialization
	void Start ()
	{
		audioSource = this.GetComponent<AudioSource> ();
		audioSource.maxDistance = beaconMaxDistance;
		audioSource.minDistance = beaconMinDistance;
		audioSource.clip = beaconSound;
		StartCoroutine (playSoundBeacon ());
	}

	IEnumerator playSoundBeacon()
	{
		while (true)
		{
			yield return new WaitForSeconds (beaconTime);
			audioSource.Play ();
			beaconLight.SetActive (true);
			beaconLight.transform.LookAt (globalOptions.playerShip.transform);
			//Debug.Log ("beacon play");
		}
	}
}
