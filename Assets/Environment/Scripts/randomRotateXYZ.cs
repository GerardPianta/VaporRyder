using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomRotateXYZ : MonoBehaviour {

	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	public float minZ;
	public float maxZ;

	private float rotX;
	private float rotY;
	private float rotZ;
	// Use this for initialization
	void Start ()
	{
		rotX = Random.Range (minX, maxX);
		rotY = Random.Range (minY, maxY);
		rotZ = Random.Range (minZ, maxZ);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (rotX,rotY,rotZ*Time.deltaTime);	
	}
}
