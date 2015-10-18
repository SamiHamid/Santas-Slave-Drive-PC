using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

	
	void Start () 
	{
		Invoke ("AudioPlay", 4.5f);
		Invoke ("LoadNextScene", 17.5f);
	}
	
	void AudioPlay()
	{
		GetComponent<AudioSource>().Play ();
	}
	
	void LoadNextScene()
	{
		Application.LoadLevel("_Scene01");
		Debug.Log("LOADING NEXT SCENE");
	}
}
