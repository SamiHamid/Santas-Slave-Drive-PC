using UnityEngine;
using System.Collections;

public class PushedSoundScript : MonoBehaviour {

	public AudioClip[] BeingPushed;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.B)) {
			BeingPushedSound ();
		}
	}

	public void BeingPushedSound()
	{
		AudioSource audio = GetComponent<AudioSource>();
		AudioClip SoundToPlay = BeingPushed [Random.Range (0, BeingPushed.Length)];
		audio.clip = SoundToPlay;
		AudioSource.PlayClipAtPoint (SoundToPlay, transform.position, 0.5f);
	}
}
