using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShip_Control : MonoBehaviour
{
	//Public variables set on the prefab
	public float player_Thrust;
	public float player_Boost;
	public float player_Break;
	public float player_Torque;
	public float fuelModifier;
	public float player_BoostTime;
	public ParticleSystem[] ThrusterFX;
	public ParticleSystem[] BoosterFX;
	public GameObject playerExplosion;
	public GameObject player_FuelGauge;
	//Audio variables
	public AudioSource player_AudioSource;
	public AudioClip audio_Explosion;
	public AudioClip audio_Thrust;
	public AudioClip audio_Boost;
	public AudioClip audio_LowFuel;
	public AudioClip audio_Error;

	private float player_Mass;
	private float player_AngularSpeed; 
	private float player_Speed;
	private float player_DotProduct;
	private Rigidbody player_RB;

	private float input_Thrust;
	private float input_Break;
	private float input_Rotate;
	private bool input_Boost;

	private GameObject uiManager;
	private bool movementEnabled = true;
	private bool playerDead = false;
	bool playerBoosting = false;
	private bool thrustAudioPlaying = false;
	private bool errorAudioPlaying = false;

	// Use this for initialization
	void Start ()
	{
		globalOptions.playerShip = this.gameObject;
		player_RB = this.GetComponent<Rigidbody> ();
		player_Mass = player_RB.mass;
		player_AudioSource = GetComponent<AudioSource> ();
		player_FuelGauge = GameObject.Find ("fuelGauge_Full");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (movementEnabled)
		{
			input_Thrust = Input.GetAxis ("Thrust");
			input_Rotate = Input.GetAxis ("Rotate");
			input_Break = Input.GetAxis ("Break");
			input_Boost = Input.GetButton ("Boost");
		}
	}

	void FixedUpdate()
	{
		if (movementEnabled)
		{
			player_AngularSpeed = player_RB.angularVelocity.magnitude;
			player_Speed = player_RB.velocity.magnitude;
			player_DotProduct = Vector3.Dot (transform.forward, Vector3.Normalize (player_RB.velocity));

			//Player input
			if (input_Thrust > 0)
			{
				MoveShip ();
			}
			else
			{
				ThrusterAudioStop ();
				ThrusterFXStop ();
			}

			if (input_Rotate != 0)
			{
				RotateShip ();
			}

			if (input_Break > 0)
			{
				BreakShip ();
			}

			if (input_Boost == true && playerBoosting == false)
			{
				if (globalOptions.uiManager.GetComponent<inGame_UIManager> ().CheckBoosterCount () > 0)
				{
					StartCoroutine (BoostShip ());
				}
				else
				{
					if (errorAudioPlaying == false)
					{
						soundManager.PlaySound2D (audio_Error);
						StartCoroutine (ErrorTimer());
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "lethalHazard")
		{
			if (playerDead == false)
				PlayerDeath ();
		}
	}

	void MoveShip ()
	{
		float speedModifier;
		speedModifier = (player_Speed / fuelModifier);
		if (player_FuelGauge.GetComponent<fuelGauge> ().CheckFuelLevel () == true)
		{
			ThrusterFXStart ();
			ThrusterAudioStart ();
			player_FuelGauge.GetComponent<fuelGauge> ().SpendFuel (speedModifier);
			player_RB.AddRelativeForce (Vector3.forward * player_Thrust);
		}
		else
		{
			ThrusterAudioStop ();
			ThrusterFXStop ();
		}
	}

	void BreakShip ()
	{
		if (player_DotProduct > 0)
		{
			float speedModifier;
			speedModifier = (player_Speed / fuelModifier);
			//player_FuelGuage.GetComponent<fuelGuage> ().SpendFuel (speedModifier);
			if (player_FuelGauge.GetComponent<fuelGauge> ().CheckFuelLevel () == true)
			{
				player_FuelGauge.GetComponent<fuelGauge> ().SpendFuel (speedModifier);
				player_RB.AddRelativeForce (Vector3.forward * (player_Break * -1));
			}
		}
	}

	void RotateShip()
	{
		float torqueModifier;
		torqueModifier = (player_AngularSpeed / fuelModifier);
		if (player_FuelGauge.GetComponent<fuelGauge> ().CheckFuelLevel () == true)
		{
			player_FuelGauge.GetComponent<fuelGauge> ().SpendFuel (torqueModifier);
			player_RB.AddTorque (Vector3.up * (player_Torque * input_Rotate));
		}
	}

	IEnumerator BoostShip()
	{
		playerBoosting = true;
		BoosterFXStart ();
		soundManager.PlaySound2D (audio_Boost);
		globalOptions.uiManager.GetComponent<inGame_UIManager> ().SpendBooster ();
		float timer = 0;
		while (timer < player_BoostTime)
		{
			timer += Time.deltaTime;
			player_RB.AddRelativeForce (Vector3.forward * player_Boost);
		}
		yield return new WaitForSeconds (player_BoostTime + 0.1f);
		playerBoosting = false;
	}

	IEnumerator ErrorTimer()
	{
		errorAudioPlaying = true;
		yield return new WaitForSeconds (audio_Error.length);
		errorAudioPlaying = false;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	void ThrusterAudioStart()
	{
		if (thrustAudioPlaying == false)
		{
			this.GetComponent<AudioSource> ().Play ();
			thrustAudioPlaying = true;
		}
	}

	void ThrusterAudioStop()
	{
		this.GetComponent<AudioSource> ().Stop();
		thrustAudioPlaying = false;
	}

	void ThrusterFXStart()
	{
		foreach (ParticleSystem Thruster in ThrusterFX)
		{
			Thruster.Play ();
		}
	}

	void ThrusterFXStop()
	{
		foreach (ParticleSystem Thruster in ThrusterFX)
		{
			Thruster.Stop ();
		}
	}

	void BoosterFXStart()
	{
		foreach (ParticleSystem Booster in BoosterFX)
		{
			Booster.Play();
		}
	}

	public void PlayerDeath()
	{
		while (playerDead == false)
		{
			playerDead = true;
			movementEnabled = false;
			ThrusterAudioStop ();
			soundManager.PlaySound (audio_Explosion, GetPosition (), 1);
			GameObject explosion = Instantiate (playerExplosion) as GameObject;
			explosion.transform.position = transform.position;
			ParticleSystem explosionParticle;
			explosionParticle = explosion.GetComponent<ParticleSystem> ();
			explosionParticle.Play ();
			HidePlayerShip ();
			globalOptions.uiManager.GetComponent<inGame_UIManager> ().GameOverScreen ();
			globalOptions.ResetGame ();
		}
	}

	public void HidePlayerShip()
	{
		foreach (Transform child in transform)
		{
			child.gameObject.SetActive (false);
		}
	}
}