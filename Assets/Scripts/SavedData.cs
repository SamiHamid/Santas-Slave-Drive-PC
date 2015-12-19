using UnityEngine;
using System.Collections;

public class SavedData : MonoBehaviour {

	public int currentLevel = 1;



	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(transform.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
