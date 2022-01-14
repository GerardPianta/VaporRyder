using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid_Behavior : MonoBehaviour
{
	//Public variables set on the prefab
	public float localScale;
	public GameObject asteroidExplosion;
	public GameObject[] asteroidChunks;
	public AudioClip audio_asteroidExplosion;

	private float randomSpeedMin;
	private float randomSpeedMax;
	private float scaleMin;
	private float scaleMax;
	private Rigidbody asteroid_RB;
	public GameObject player;

	//Notes
	//put asteroid chunk generation into a coroutine to delay the creation
	//add a speed modifier that is based on the game level (possibly difficulty as well) to procedurally increase the speed depending on the level.

	// Use this for initialization
	void Start ()
	{
		globalOptions.asteroidCount++;	
		player = globalOptions.playerShip;
		randomSpeedMin = globalOptions.asteroid_MinSpeed;
		randomSpeedMax = globalOptions.asteroid_MaxSpeed;
		scaleMin = globalOptions.asteroid_MinSize;
		scaleMax = globalOptions.asteroid_MaxSize;
		asteroid_RB = GetComponent<Rigidbody> ();
		asteroid_RB.velocity = RandomVector(randomSpeedMin, randomSpeedMax);
		float randScale = Random.Range (scaleMin, scaleMax);
		this.transform.localScale = new Vector3(randScale, randScale, randScale);
		localScale = randScale;
	}

	private Vector3 RandomVector(float min, float max)
	{
		float x = Random.Range (min, max);
		float z = Random.Range (min, max);
		return new Vector3 (x, 0, z);
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}

	public Vector3 RandomizePosition()
	{
		return new Vector3 (transform.position.x + (Random.Range(-5,5)), 0, transform.position.z + (Random.Range(-5,5)));
	}

	public float GetDotProduct()
	{
		return Vector3.Dot (transform.forward, Vector3.Normalize (asteroid_RB.velocity));
	}

	void OnCollisionEnter(Collision collision)
	{
		AsteroidExplode ();
	}

	void AsteroidExplode()
	{
		int arrayLength = (asteroidChunks.Length-1);
		int asteroidChunkIndex;
		GameObject explosion = Instantiate (asteroidExplosion) as GameObject;
		explosion.transform.position = GetPosition ();
		explosion.transform.localScale = new Vector3 (localScale / 10, localScale / 10, localScale / 10);
		ParticleSystem explosionParticle;
		explosionParticle = explosion.GetComponent<ParticleSystem>();
		explosionParticle.Play();
		int numMod = Mathf.Clamp (((int)localScale/10), 0, ((int)scaleMax / 10));
		int randInt = (Random.Range (3, 5) + numMod);
		for (int i = 0; i < randInt; i++)
		{
			asteroidChunkIndex = Random.Range (0, arrayLength);
			GameObject chunk = Instantiate (asteroidChunks[asteroidChunkIndex], RandomizePosition(), Quaternion.identity) as GameObject;
			float currentScale = chunk.transform.localScale.x;
			float randRangeMin = localScale* 0.4f;
			float randRangeMax = localScale * 0.75f;
			float newScale = Random.Range (randRangeMin, randRangeMax);
			chunk.transform.localScale = new Vector3 (newScale, newScale, newScale);
		}
		float randomPitch = Random.Range (0.5f, 0.95f);
		soundManager.PlaySound (audio_asteroidExplosion, GetPosition(), randomPitch);
		gameObject.GetComponent<hazard_DestroySelf> ().DestroySelf ();
	}
}