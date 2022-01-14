using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class renderTextureChanger : MonoBehaviour
{
	public RenderTexture[] resolutionTexture;
	public int currentResolution;

	//Notes:
	//Control the in game resolution in its own function to imporove efficiency

	// Use this for initialization
	void Start ()
	{
		currentResolution = globalOptions.resolutionLevel;
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentResolution = globalOptions.resolutionLevel;
		GetComponent<RawImage>().texture = resolutionTexture [currentResolution];
		//GetComponent<Image> ().material.mainTexture = resolutionTexture [currentResolution];
	}
}
