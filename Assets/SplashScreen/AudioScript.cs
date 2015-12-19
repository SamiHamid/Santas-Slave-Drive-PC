using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

	
	void Start () 
	{
		Invoke ("AudioPlay", 0f);
		Invoke ("LoadNextScene", 13f);
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
