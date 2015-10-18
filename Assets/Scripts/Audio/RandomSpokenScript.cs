using UnityEngine;
using System.Collections;

public class RandomSpokenScript : MonoBehaviour 
{
	public AudioClip[] RandomlySpokenPeace;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			RandomlySpokenP();
		}
	}

	public void RandomlySpokenP()
	{
		AudioSource audio = GetComponent<AudioSource>();
		AudioClip SoundToPlay = RandomlySpokenPeace [Random.Range (0, RandomlySpokenPeace.Length)];
		audio.clip = SoundToPlay;
		AudioSource.PlayClipAtPoint (SoundToPlay, transform.position, 0.5f);
	}
}
