using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energyWave_Behavior : MonoBehaviour {

	public GameObject player_FuelGauge;
	public float fuelPenalty;

	// Use this for initialization
	void Start ()
	{
		player_FuelGauge = GameObject.Find ("fuelGauge_Full");
	}

	void OnCollisionEnter(Collision collision)
	{
		GameObject player = globalOptions.playerShip;
		if (collision.gameObject == player)
		{
			player_FuelGauge.GetComponent<fuelGauge> ().SpendFuel (fuelPenalty);
			if (player_FuelGauge.GetComponent<fuelGauge> ().CheckFuelLevel () == false)
			{
				player.GetComponent<playerShip_Control> ().PlayerDeath ();
			}
		}
	}
}
