using UnityEngine;
using System.Collections;

public class AcknowledgeSoundScript : MonoBehaviour {

	public AudioClip[] Acknowledgements;

	void Start () {
	
	}
	public void Update()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			AcknowledgeSound ();
		}
	}

	public void AcknowledgeSound ()
	{   
		AudioSource audio = GetComponent<AudioSource>();
		AudioClip SoundToPlay = Acknowledgements [Random.Range (0, Acknowledgements.Length)];
		audio.clip = SoundToPlay;
		AudioSource.PlayClipAtPoint (SoundToPlay, transform.position, 0.5f);
	}
}
