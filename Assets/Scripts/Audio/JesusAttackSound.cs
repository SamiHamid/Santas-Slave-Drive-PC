using UnityEngine;
using System.Collections;

public class JesusAttackSound : MonoBehaviour 
{
	public AudioClip[] JesusAttack;

	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			JesusAttackSnd();
		}	
	}

	public void JesusAttackSnd()
	{
		AudioSource audio = GetComponent<AudioSource>();
		AudioClip SoundToPlay = JesusAttack [Random.Range (0, JesusAttack.Length)];
		audio.clip = SoundToPlay;
		AudioSource.PlayClipAtPoint (SoundToPlay, transform.position, 0.5f);
	}
}
