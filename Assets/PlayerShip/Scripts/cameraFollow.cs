using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour {
	public GameObject playerShip;
	
	// Update is called once per frame
	void Update ()
	{
		this.transform.position = new Vector3 (playerShip.transform.position.x, this.transform.position.y, playerShip.transform.position.z);
	}
}
