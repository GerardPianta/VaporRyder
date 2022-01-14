using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameObjectDeactivateTimer : MonoBehaviour {
	public GameObject thisObject;
	public float deactivateTimer;

	void OnEnable ()
	{
		//thisObject = this.gameObject;
		StartCoroutine (deactivateSelf ());
	}

	IEnumerator deactivateSelf()
	{
		yield return new WaitForSeconds (deactivateTimer);
		this.gameObject.SetActive (false);
	}
	

}
