using UnityEngine;
using System.Collections;

public class randomRotateZ : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	transform.Rotate (0,0,Random.Range( 0.25f, 0.65f )*Time.deltaTime);
	}
}
