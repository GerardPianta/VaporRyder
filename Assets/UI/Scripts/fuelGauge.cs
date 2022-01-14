using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fuelGauge : MonoBehaviour
{
	//Public variables set on the prefab
	public GameObject fuelSlider;
	public float fuelMin;
	public float fuelMax;
	public float fuelWarningLevel = .25f;
	[Range(0f, 10f)]
	public float fuelLevel;
	public float fuelWarningDelay;

	public float normalizedFuelRange;

	//Notes
	//Implement fuel warning audio

	void Update()
	{
		globalOptions.fuelLeft = fuelLevel;
	}
	void Start()
	{
		globalOptions.playerFuelGauge = this.gameObject;
	}

	public void SpendFuel(float fuelBurn)
	{
		if (fuelLevel > 0)
		{
			fuelLevel = fuelLevel - fuelBurn;
			normalizedFuelRange = (fuelLevel - fuelMin) / (fuelMax - fuelMin);
			fuelSlider.GetComponent<Image>().fillAmount = normalizedFuelRange;
		}
	}

	public bool CheckFuelLevel()
	{
		if (fuelLevel > 0)
		{
			return true;
		}
		return false;
	}

	/*IEnumerator LowFuel ()
	{
		while (fuelLevel < fuelWarningLevel)
		{
			yield return new WaitForSeconds (fuelWarningDelay);
		}
	}*/
}
