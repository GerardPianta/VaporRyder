using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidChunk_Behavior : MonoBehaviour
{
	//Public variables set on the prefab
	public float randomSpeedMin;
	public float randomSpeedMax;
	public GameObject asteroidChunkExplosion;
	private Rigidbody asteroid_RB;
	public AudioClip audio_asteroidExplosion;

	private GameObject player;

	//Notes
	//add a speed modifier that is based on the game level (possibly difficulty as well) to procedurally increase the speed depending on the level.

	// Use this for initialization
	void Start ()
	{
		globalOptions.asteroidChunkCount++;
		player = globalOptions.playerShip;
		asteroid_RB = GetComponent<Rigidbody> ();
		asteroid_RB.velocity = RandomVector(randomSpeedMin, randomSpeedMax);
	}

	private void OnTriggerEnter(Collider collider)
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		AsteroidExplode ();
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	private Vector3 RandomVector(float min, float max)
	{
		float x = Random.Range (min, max);
		float z = Random.Range (min, max);
		return new Vector3 (x, 0, z);
	}
	
	void AsteroidExplode()
	{
		//spawns an explosion particle effect with its scale based on the asteroid's scale
		float currentScale = gameObject.transform.localScale.x;
		float newScale = currentScale / 7.5f;
		GameObject explosion = Instantiate (asteroidChunkExplosion) as GameObject;
		explosion.transform.position = GetPosition ();
		explosion.transform.localScale = new Vector3 (newScale, newScale, newScale);
		ParticleSystem explosionParticle;
		explosionParticle = explosion.GetComponent<ParticleSystem>();
		explosionParticle.Play();
		float randomPitch = Random.Range (0.9f, 1.25f);
		soundManager.PlaySound (audio_asteroidExplosion, GetPosition(), randomPitch);
		gameObject.GetComponent<hazard_DestroySelf> ().DestroySelf ();
	}

	void DestroySelf()
	{
		globalOptions.asteroidChunkCount--;
		Destroy(this.gameObject);
	}
}
