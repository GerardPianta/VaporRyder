using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenuNav : MonoBehaviour
{
	public int menuIndex;
	public GameObject[] menuItems;
	public GameObject menuOpen;
	public GameObject menuClose;
	public int menuLength;
	public Color buttonNormal;
	public Color buttonSelect;
	public Color buttonPress;

	public float testInput;

	public float submitPressDelay = .2f;
	public float navDelay = .15f;

	private bool menuScrolling = false;

	//menu indexes
	private int index_startGame = 0;
	private int index_options = 1;
	private int index_exit = 2;

	void Awake ()
	{
		menuLength = (menuItems.Length-1);
		menuIndex = 0;
		//menuItems [menuIndex].GetComponent<Image> ().color = buttonSelect;
		menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
	}

	void Start()
	{
		GameObject mainCamera = GameObject.Find ("Main Camera");
		mainCamera.GetComponent<AudioSource> ().Play ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetAxis ("Submit") == 1)
		{
			//menuItems [menuIndex].GetComponent<Image> ().color = buttonPress;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonPress;
			StartCoroutine ("SubmitPressWait");
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

	IEnumerator SubmitPressWait ()
	{
		yield return new WaitForSeconds (submitPressDelay);
		menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;

		if (menuIndex == index_startGame)
		{
			SceneManager.LoadScene ("playground");
		}
		else
		if (menuIndex == index_options)
		{
			menuOpen.SetActive(true);
			menuItems [menuIndex].GetComponent<Text> ().color = buttonNormal;
			menuIndex = 0;
			menuItems [menuIndex].GetComponent<Text> ().color = buttonSelect;
			menuClose.SetActive(false);
		}
		else
		if (menuIndex == index_exit)
		{
			Application.Quit();
			Debug.Log ("The user has quit the game!");
		}
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
}