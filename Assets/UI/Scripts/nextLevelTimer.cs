using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLevelTimer : MonoBehaviour {

	public float nexLevelTime = 5f;
	public GameObject globals;

	// Use this for initialization
	void Start ()
	{
		globals = GameObject.Find ("Globals");
		StartCoroutine ("LoadLevel");
	}

	IEnumerator LoadLevel()
	{
		globalOptions.gameLevel++;
		yield return new WaitForSeconds (nexLevelTime);
		SceneManager.LoadScene ("playground");
		globals.GetComponent<globalOptions> ().LevelDefaults ();
	}
}
