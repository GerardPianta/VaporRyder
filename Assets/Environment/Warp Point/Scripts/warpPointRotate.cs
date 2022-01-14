using UnityEngine;
using System.Collections;

public class warpPointRotate : MonoBehaviour {

public GameObject innerRing;
public GameObject lattice01;
public GameObject lattice02;
public GameObject middleRing;
public GameObject outerRing01;
public GameObject outerRing02;

public float innerRingRot;
public float middleRingRot;
public float outerRing01Rot;
public float outerRing02Rot;

	// Use this for initialization
	void Start ()
	{
		 
	}

	// Update is called once per frame
	void Update ()
	{
		innerRing.transform.Rotate (0,0,innerRingRot*Time.deltaTime);
		lattice01.transform.Rotate (0,0,innerRingRot*Time.deltaTime);
		lattice02.transform.Rotate (0,0,innerRingRot*Time.deltaTime);
		middleRing.transform.Rotate (0,0,middleRingRot*Time.deltaTime);
		outerRing01.transform.Rotate (0,0,outerRing01Rot*Time.deltaTime);
		outerRing02.transform.Rotate (0,0,outerRing02Rot*Time.deltaTime);
	}
}
