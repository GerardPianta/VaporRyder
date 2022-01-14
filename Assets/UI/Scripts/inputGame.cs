using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputGame : MonoBehaviour
{	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton ("Exit"))
		{
			SceneManager.LoadScene ("MainMenu");
			//Debug.Log ("The user has quit the game!");
		}
	}
}
