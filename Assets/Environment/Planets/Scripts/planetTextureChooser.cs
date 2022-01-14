using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetTextureChooser : MonoBehaviour {
	public Texture2D[] planetTextures;
	Texture2D planetTexture;
	int arrayLength;
	int textureIndex;

	// Use this for initialization
	void Start ()
	{
		arrayLength = (planetTextures.Length-1);
		textureIndex = Random.Range (0, arrayLength);
		planetTexture = planetTextures [textureIndex];
		GetComponent<Renderer>().material.mainTexture = planetTexture;
	}
}
