using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole_Behavior : MonoBehaviour {

	public GameObject player;

	//physics variables
	public float randomSpeedMin;
	public float randomSpeedMax;
	public Rigidbody blackHole_RigidBody;
	public float attractionDistance = 15;
	float beginWarpDistance;
	public float eventHorizonSize = 1;
	public float minTeleportDistance = 15;
	public float maxTeleportDistance = 50;
	public bool isActive = true;

	public GameObject teleportFX;

	public AudioClip audio_blackHoleLoop;
	public AudioClip audio_Teleport;

	void Start ()
	{
		beginWarpDistance = attractionDistance / 2;
		player = globalOptions.playerShip;
		blackHole_RigidBody = this.GetComponent<Rigidbody> ();
		globalOptions.blackHoleCount++;
		RandomVectorForce ();
	}

	public void RandomVectorForce()
	{
		blackHole_RigidBody.velocity = RandomVector(randomSpeedMin, randomSpeedMax);
	}

	private Vector3 RandomVector(float min, float max)
	{
		float x = Random.Range (min, max);
		float z = Random.Range (min, max);
		return new Vector3 (x, 0, z);
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public Vector3 GetPosition(GameObject go)
	{
		return go.transform.position;
	}

	void FixedUpdate()
	{
		if (isActive == true)
		{
			Collider[] attractObjects = Physics.OverlapSphere (GetPosition (), attractionDistance);
			foreach (Collider attractedObject in attractObjects)
			{
				if ((attractedObject.gameObject.tag == "lethalHazard") || (attractedObject.gameObject.tag == "Player"))
				{
					Attract (attractedObject.gameObject);
				}
			}
			Collider[] teleportObjects = Physics.OverlapSphere (GetPosition (), eventHorizonSize);
			foreach (Collider teleportObject in teleportObjects)
			{
				if ((teleportObject.gameObject.tag == "lethalHazard") || (teleportObject.gameObject.tag == "Player"))
				{
					Teleport (teleportObject.gameObject);
				}
			}
		}
	}

	public void Attract(GameObject go)
	{
		Rigidbody attractedObjectRB = go.GetComponent<Rigidbody> ();
		Vector3 direction = blackHole_RigidBody.position - attractedObjectRB.position;
		float speed = attractedObjectRB.velocity.magnitude;
		float distance = direction.magnitude;
		float forceMagnitude = (blackHole_RigidBody.mass * attractedObjectRB.mass) / Mathf.Pow (distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;
		if (go == globalOptions.playerShip)
			attractedObjectRB.AddForce (force);
		if ((go.tag == "lethalHazard") && (speed < globalOptions.maxSpeedFromBlackHole))
			attractedObjectRB.AddForce (force);
		//Debug.Log (go + " gets " + force + " force added to it");
	}

	void ScaleGameObject (GameObject go, float distance)
	{
		float scale = Mathf.Clamp ((distance / beginWarpDistance), 0.05f, 1);
		go.transform.localScale = new Vector3 (scale, scale, scale);
	}

	public void Teleport(GameObject go)
	{
		Vector3 newPos;
		bool playerInRange = true;
		while (playerInRange == true)
		{
			float ranDist = Random.Range (minTeleportDistance, maxTeleportDistance);
			Vector3 randPos = Random.insideUnitSphere * ranDist;
			newPos = new Vector3 (randPos.x, 0, randPos.z);
			if (Vector3.Distance (GetPosition (globalOptions.playerShip), newPos) > 15)
			{
				go.transform.position = newPos;
				soundManager.PlaySound (audio_Teleport, newPos, 1);
				GameObject warpFX = Instantiate (teleportFX) as GameObject;
				warpFX.transform.position = newPos;
				playerInRange = false;
			}
		}
	}
}
