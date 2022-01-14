using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class warpPoint_Behavior : MonoBehaviour {
	
	//Public variables set on the prefab
	public float attractionDistance;
	public float beginWarpDistance;
	public float warpDistance;
	public GameObject teleportFX;
	public AudioClip audio_Warp;

	private Rigidbody warpPoint_Rigidbody;
	private GameObject player;
	private bool playerWarping = false;

	// Use this for initialization
	void Start ()
	{
		player = globalOptions.playerShip;
		globalOptions.warpPoint = this.gameObject;
		warpPoint_Rigidbody = this.GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		Collider[] attractObjects = Physics.OverlapSphere (GetPosition (), attractionDistance);
		foreach (Collider attractedObject in attractObjects)
		{
			if (attractedObject.gameObject.tag == "Player")
			{
				//Debug.Log (attractedObject);
				Attract (attractedObject.gameObject);
			}
		}
		Collider[] warpingObjects = Physics.OverlapSphere (GetPosition (), beginWarpDistance);
		foreach (Collider warpingObject in warpingObjects)
		{
			if (warpingObject.gameObject.tag == "Player")
			{
				float distance = Vector3.Distance (warpingObject.transform.position, transform.position);
				ScalePlayer (warpingObject.gameObject, distance);
				if (distance <= warpDistance)
				{
					if (playerWarping == false)
						StartCoroutine (ExitLevel (warpingObject.gameObject));
				}
			}
		}
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public void Attract(GameObject go)
	{
		Rigidbody attractedObjectRB = go.GetComponent<Rigidbody> ();
		Vector3 direction = warpPoint_Rigidbody.position - attractedObjectRB.position;
		float distance = direction.magnitude;
		float forceMagnitude = (warpPoint_Rigidbody.mass * attractedObjectRB.mass) / Mathf.Pow (distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;
		attractedObjectRB.AddForce (force);
	}

	void ScalePlayer (GameObject go, float distance)
	{
		float scale = Mathf.Clamp ((distance / beginWarpDistance), 0.05f, 1);
		go.transform.localScale = new Vector3 (scale, scale, scale);
	}

	IEnumerator ExitLevel (GameObject go)
	{
		playerWarping = true;
		GameObject warpFX = Instantiate (teleportFX) as GameObject;
		warpFX.transform.position = gameObject.transform.position;
		Rigidbody playerRigidBody = go.GetComponent<Rigidbody> ();
		playerRigidBody.constraints = RigidbodyConstraints.FreezeAll;
		soundManager.PlaySound (audio_Warp, GetPosition (), 1);
		globalOptions.playerShip.GetComponent<playerShip_Control> ().HidePlayerShip ();
		globalOptions.uiManager.GetComponent<inGame_UIManager> ().CompleteLevel ();
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene ("LevelComplete");
	}

}