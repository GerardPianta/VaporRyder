using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenFlicker : MonoBehaviour
{
	public bool flickerEnabled = true;
	public bool flicker_on = false;
	public float flickerMin;
	public float flickerMax;
	public float flickerTime;
	public float flickerTimeMin;
	public float flickerTimeMax;
	public GameObject flickerPanel;
	public Image flickerPanelImage;
	public Color flickerColor;


	//notes: add functioin to slowly change the flicker color

	// Use this for initialization
	void Start ()
	{
		if (flickerEnabled == true) {
			StartCoroutine (ScreenFlicker ());
		}
	}

	void Update ()
	{
		//Color imageColor = GetComponent<Image>().color;
		//imageColor.a = Mathf.Lerp(imageColor.a, Random.Range (flickerMin, flickerMax), Time.deltaTime);
	}

	IEnumerator ScreenFlicker()
	{
		while (flickerEnabled)
		{
			yield return new WaitForSeconds (flickerTime);
			if (flicker_on)
			{
				flickerColor.a = Random.Range (flickerMin, flickerMax);
				//Debug.Log ("The flicker is on");
			}
			else
			{
				flickerColor.a = flickerMin;
				//Debug.Log ("The flicker is off");
			}
			GetComponent<Image> ().color = flickerColor;
			flicker_on = !flicker_on;
		}
	}
}
