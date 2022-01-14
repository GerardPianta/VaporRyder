using UnityEngine;
using System.Collections;

public class randomRotateY : MonoBehaviour
{
	float rotY;
	public float minY;
	public float maxY;
	// Use this for initialization
	void Start ()
	{
		rotY = Random.Range (minY, maxY);
	}
	
	// Update is called once per frame
	void Update () {
	transform.Rotate (0,rotY,0*Time.deltaTime);
	}
}
