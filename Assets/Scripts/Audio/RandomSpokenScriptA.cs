using UnityEngine;
using System.Collections;

public class RandomSpokenScriptA : MonoBehaviour 
{
	public AudioClip[] RandomlySpokenAttack;

	void Update () 
	{
		
		if (Input.GetKeyDown(KeyCode.E))
		{
			RandomlySpokenA();
		}
	}

	public void RandomlySpokenA()
	{
		AudioSource audio = GetComponent<AudioSource>();
		AudioClip SoundToPlay = RandomlySpokenAttack [Random.Range (0, RandomlySpokenAttack.Length)];
		audio.clip = SoundToPlay;
		AudioSource.PlayClipAtPoint (SoundToPlay, transform.position, 0.5f);
	}
}
