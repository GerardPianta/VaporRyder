using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sputnik_Behavior : MonoBehaviour {

	//Public variables set on the prefab
	public GameObject sputnikProbe;
	public GameObject energyWave;
	public float minSpeed;
	public float maxSpeed;
	public GameObject spaceJunkExplosion;
	public AudioClip audio_EnergyWave;
	public AudioClip audio_Explosion;

	private Rigidbody sputnik_RB;
	//Notes
	//add a speed modifier that is based on the game level (possibly difficulty as well) to procedurally increase the speed depending on the level.
	//Add sound effect to the energy wave

	// Use this for initialization
	void Start ()
	{
		globalOptions.spaceJunkCount++;
		sputnik_RB = gameObject.GetComponent<Rigidbody> ();
		//sets a random cool down rate for the energy burst 
		float randomCoolDown = Random.Range (0.15f, 1.5f);
		gameObject.GetComponent<Animator> ().SetFloat ("coolDown", randomCoolDown);

		//randomly rotates and sets the velocity of the sputnik
		float randomRotate = Random.Range (0, 360);
		transform.Rotate (0.0f, randomRotate, 0.0f, Space.World);
		float randomSpeed = Random.Range (minSpeed, maxSpeed);
		sputnik_RB.velocity = transform.forward * randomSpeed;
	}

	void OnCollisionEnter(Collision collision)
	{
		SputnikExplode (collision.gameObject);
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public void SputnikExplode(GameObject go)
	{
		if (go.tag == "lethalHazard")
		{
			GameObject explosion = Instantiate (spaceJunkExplosion) as GameObject;
			explosion.transform.position = GetPosition ();
			ParticleSystem explosionParticle;
			explosionParticle = explosion.GetComponent<ParticleSystem> ();
			explosionParticle.Play ();
			float randomPitch = Random.Range (0.75f, 1.3f);
			soundManager.PlaySound (audio_Explosion, GetPosition (), randomPitch);
			gameObject.GetComponent<hazard_DestroySelf> ().DestroySelf ();
		}
	}

	public void playEnergyWaveAudioClip()
	{
		soundManager.PlaySound (audio_EnergyWave, GetPosition (), 1);
	}
}