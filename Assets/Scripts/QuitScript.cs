﻿using UnityEngine;
using System.Collections;

public class QuitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
			Debug.Log("QUIT");
		}
	}
}
