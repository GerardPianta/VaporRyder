using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestroySelf : MonoBehaviour
{
	public float destroySelfTimer = 2f;

	// Use this for initialization
	void Start () {
		StartCoroutine (destroySelf ());
	}

	IEnumerator destroySelf()
	{
		yield return new WaitForSeconds (destroySelfTimer);
			Destroy(this.gameObject);
	}
}
