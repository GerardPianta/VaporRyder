using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceField_Behavior : MonoBehaviour {

	//Public variables set on the prefab
	public GameObject forceFieldStatic;
	public GameObject forceFieldHit;
	public float forceFieldDisplayTime;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("IgnorePlayer");
	}

	/*private void OnTriggerEnter(Collider collider)
	{
		GameObject collisionObject = collider.gameObject;
		if (collisionObject.CompareTag("asteroid"))
		{
			StartCoroutine(collisionObject, ActivateForceField(collisionObject.transform.position));
		}
	}*/



	void OnCollisionEnter(Collision collision)
	{
		GameObject collisionObject = collision.gameObject;
		if (collisionObject.CompareTag("lethalHazard"))
		{
			StartCoroutine(ActivateForceField(collisionObject.transform.position));
		}
		if (collisionObject.CompareTag ("blackHole"))
		{
			StartCoroutine(collisionObject.GetComponent<blackHole_warpBehavior>().BlackHoleDisappear());
		}
	}

	public Vector3 GetPosition(GameObject go)
	{
		return go.transform.position;
	}

	IEnumerator IgnorePlayer()
	{
		yield return new WaitForSeconds (0.1f);
		Physics.IgnoreCollision (globalOptions.playerShip.GetComponent<Collider> (), GetComponent<Collider> ());
	}

	IEnumerator ActivateForceField(Vector3 position)
	{
		//forceFieldHit.transform.LookAt (position);
		forceFieldStatic.GetComponent<MeshRenderer> ().enabled = true;
		forceFieldHit.GetComponent<MeshRenderer> ().enabled = true;
		yield return new WaitForSeconds (forceFieldDisplayTime);
		forceFieldStatic.GetComponent<MeshRenderer> ().enabled = false;
		forceFieldHit.GetComponent<MeshRenderer> ().enabled = false;
	}
}