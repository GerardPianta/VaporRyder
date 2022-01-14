using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class optionsMenuNav : MonoBehaviour
{
	//Public variables set on the prefab
	public int menuIndex;
	public GameObject[] menuItems;
	public GameObject menuOpen;
	public GameObject menuClose;
	public int menuLength;
	public Color buttonNormal;
	public Color buttonSelect;
	public Color buttonPress;
	public int volumeIndex;
	public float submitPressDelay = .2f;
	public float navDelay = .15f;

	// private variables
	private bool menuScrolling = false;
	private bool buttonPressed = false;

	//menu indexes
	private int index_optionsResolution = 0;
	private int index_optionsVolume = 1;
	private int index_optionsReturn = 2;

	//resolution indexes
	private const int resLow = 0;
	private const int resMed = 1;
	private const int resHigh = 2;

	//volume indexes
	private const int index_volOff = 0;
	private const int index_volMax = 4;

	//menu text
	private string text_ResLow = "Resolution Level: 240x135";
	private string text_ResMed = "Resolution Level: 360x202";
	private string text_ResHigh = "Resolution Level: 480x270";

	//Notes:
	// make variables for menu indexes to improve code clarity
	// make function to highlight menu option
	// make function to select


	// Use this for initialization
	void Start ()
	{
		//InnitializeText ();
	}

	void Awake ()
	{
		menuLength = (menuItems.Length-1);
		menuIndex = 0;
		volumeIndex = (int)(globalOptions.volumeLevel*4);
		//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
		menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetAxis ("Submit") == 1 && buttonPressed == false)
		{
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonPress;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonPress;
			//button press function with subtle delay
			StartCoroutine ("SubmitPressWaitTest");
		}

		if ((Input.GetAxis ("Vertical") < -.25) && menuScrolling == false)
		{
			StartCoroutine ("NavDown");
		}
		if ((Input.GetAxis ("Vertical") > .25) && menuScrolling == false)
		{
			StartCoroutine ("NavUp");
		}
	}
		
	void InnitializeText()
	{
		switch (globalOptions.resolutionLevel)
		{
		case resLow:
			menuItems [index_optionsResolution].GetComponent<Text> ().text = text_ResLow;
			break;
		case resMed:
			menuItems [index_optionsResolution].GetComponent<Text> ().text = text_ResMed;
			break;
		case resHigh:
			menuItems [index_optionsResolution].GetComponent<Text> ().text = text_ResHigh;
			break;
		}

		if (volumeIndex == 0)
		{
			menuItems [index_optionsVolume].GetComponent<Text> ().text = "Volume Level: Off";
		}
		else if ((volumeIndex > 0) && (volumeIndex <= 4))
		{
			menuItems [index_optionsVolume].GetComponent<Text> ().text = "Volume Level: " + volumeIndex.ToString ();
		}
		else
		{
			Debug.LogError("volume index out of range");
		}
	}
	IEnumerator SubmitPressWaitTest ()
	{
		buttonPressed = true;
		yield return new WaitForSeconds (submitPressDelay);
		//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
		menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;

		if (menuIndex == index_optionsResolution)
		{
			SetResolution ();
		}
		else if (menuIndex == index_optionsVolume)
		{
			SetVolume ();
		}
		else if (menuIndex == index_optionsReturn)
		{
			ReturnToMainMenu ();
		}
		buttonPressed = false;
	}

	IEnumerator NavDown ()
	{
		menuScrolling = true;
		yield return new WaitForSeconds (navDelay);
		if (menuIndex < menuLength)
		{
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonNormal;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonNormal;
			menuIndex++;
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();

		}
		else
		if (menuIndex >= menuLength)
		{
				//menuItems [menuIndex].GetComponent<Image> ().color = buttonNormal;
				menuItems [menuIndex].GetComponent<Text> ().color = buttonNormal;
				menuIndex = 0;
				//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
				menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
				AudioSource audio = GetComponent<AudioSource>();
				audio.Play();
		}
		menuScrolling = false;
	}

	IEnumerator NavUp ()
	{
		menuScrolling = true;
		yield return new WaitForSeconds (navDelay);
		if (menuIndex <= 0)
		{
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonNormal;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonNormal;
			menuIndex = menuLength;
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}
		else
		if (menuIndex > 0)
		{
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonNormal;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonNormal;
			menuIndex = (menuIndex-1);
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}
		menuScrolling = false;
	}

	void SetResolution()
	{
		switch (globalOptions.resolutionLevel)
		{
		case resLow:
			menuItems [menuIndex].GetComponent<Text> ().text = text_ResMed;
			globalOptions.resolutionLevel++;
			break;
		case resMed:
			menuItems [menuIndex].GetComponent<Text> ().text = text_ResHigh;
			globalOptions.resolutionLevel++;
			break;
		case resHigh:
			menuItems [menuIndex].GetComponent<Text> ().text = text_ResLow;
			globalOptions.resolutionLevel = resLow;
			break;
		}
	}

	void SetVolume()
	{
		switch (volumeIndex)
		{
		case 0:
			globalOptions.volumeLevel = 0.25f;
			volumeIndex++;
			menuItems [menuIndex].GetComponent<Text> ().text = "Volume Level: " + volumeIndex.ToString ();
			break;
		case 1:
			globalOptions.volumeLevel = 0.5f;
			volumeIndex++;
			menuItems [menuIndex].GetComponent<Text> ().text = "Volume Level: " + volumeIndex.ToString ();
			break;
		case 2:
			globalOptions.volumeLevel = 0.75f;
			volumeIndex++;
			menuItems [menuIndex].GetComponent<Text> ().text = "Volume Level: " + volumeIndex.ToString ();
			break;
		case 3:
			globalOptions.volumeLevel = 1.0f;
			volumeIndex++;
			menuItems [menuIndex].GetComponent<Text> ().text = "Volume Level: " + volumeIndex.ToString ();
			break;
		case 4:
			globalOptions.volumeLevel = 0f;
			menuItems [menuIndex].GetComponent<Text> ().text = "Volume Level: Off";
			volumeIndex = 0;
			break;
		}
		globalOptions.UpdateVolume ();
	}

	void ReturnToMainMenu()
	{
		menuOpen.SetActive(true);
		menuItems [menuIndex].GetComponent<Text> ().color = buttonNormal;
		menuIndex = 0;
		menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
		menuClose.SetActive(false);
	}
}