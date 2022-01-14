using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
	//Notes
	//test the distance to the player before playing a sound

	public static void PlaySound(AudioClip sound, Vector3 position, float pitch)
	{
		GameObject soundObject = new GameObject (sound + "_Sound");
		soundObject.transform.position = position;
		AudioSource audioSource = soundObject.AddComponent<AudioSource> ();
		audioSource.minDistance = 5;
		audioSource.maxDistance = 150;
		audioSource.spatialBlend = 1;
		audioSource.clip = sound;
		audioSource.Play ();
		soundObject.AddComponent<timedDestroySelf> ();
	}

	public static void PlaySound2D(AudioClip sound)
	{
		GameObject soundObject = new GameObject (sound + "_Sound");
		AudioSource audioSource = soundObject.AddComponent<AudioSource> ();
		audioSource.PlayOneShot (sound);
	}

	/*public static void PlayLoopingSound2D(AudioClip sound)
	{
		GameObject soundObject = new GameObject (sound + "_Sound");
		AudioSource audioSource = soundObject.AddComponent<AudioSource> ();
		audioSource.PlayOneShot (sound);
		StartCoroutine (SoundLoopTimer (sound, sound.length));
	}

	IEnumerator SoundLoopTimer(AudioClip sound, float soundLength)
	{
		bool audioLoopPlaying = true;
		float timer = 0;
		while (timer < soundLength)
		{
			timer += Time.deltaTime;
		}
		yield return new WaitForSeconds (soundLength);
		audioLoopPlaying = false;
	}*/
}