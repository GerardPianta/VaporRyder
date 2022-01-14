using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerExplosionDestroySelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (destroySelf ());
	}

	IEnumerator destroySelf()
	{
		yield return new WaitForSeconds (2);
			Destroy(this.gameObject);
	}
}
