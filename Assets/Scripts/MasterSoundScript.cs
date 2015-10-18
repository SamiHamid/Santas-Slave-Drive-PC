using UnityEngine;
using System.Collections;

public class MasterSoundScript : MonoBehaviour 
{
	// SOUND ACTIVATORS
	private int AcknowledgeActive = 0;
	private int BeingPushedActive = 0;
	public int AcknowledgementThreshold;
	public int BeingPushedThreshold;
	
	public GameObject AcknowledgeSoundObject;
	private AcknowledgeSoundScript AckScript;
	
	public GameObject PushedSoundObject;
	private PushedSoundScript PshdScript;

	public GameObject RandomlySpokenPeace;
	private RandomSpokenScript RandomPeaceScript;
	
	// BOOLEAN FLIPS
	private bool AckGreenLight = true;
	private bool RandomGreenLight = false;

	//RANDOMISER
	private float Probability;
	public float PerMinutePeaceTalk;

	void Start ()
	{
		Invoke ("FlipRandomGreen", 5);
	}

	void Update () 
	{
		// SOUND RESET
		if (AcknowledgeActive > 0) {AcknowledgeActive--;}
		if (BeingPushedActive > 0) {BeingPushedActive--;}


		// ACKNOWLEDGE SOUND ACTIVATED
		if (AcknowledgeActive >= AcknowledgementThreshold)
		{
			AckScript = AcknowledgeSoundObject.GetComponent<AcknowledgeSoundScript>();
			AckScript.AcknowledgeSound ();
			AcknowledgeActive = 0;
			AckGreenLight = false;
			Invoke ("FlipGreen", 1f);
		}


		// BEING PUSHED SOUND ACTIVATED
		if (BeingPushedActive >= BeingPushedThreshold)
		{
			PshdScript = PushedSoundObject.GetComponent<PushedSoundScript>();
			PshdScript.BeingPushedSound();
			BeingPushedActive = 0;
		}

		// RANDOM IN PEACE TIME SOUNDS
		Probability = Time.deltaTime * PerMinutePeaceTalk;
		if (Random.value < Probability)
		{
			if (RandomGreenLight == true);
			RandomPeaceScript = RandomlySpokenPeace.GetComponent<RandomSpokenScript>();
			RandomPeaceScript.RandomlySpokenP();
			RandomGreenLight = false;
			Invoke ("FlipRandomGreen", 2);
		}

	}

	// CALLED FROM RAYCAST FROM PLAYER OBJECT
	public void AcknowledgeActivator()
	{
		if (AckGreenLight == true) 
		{
			AcknowledgeActive+=2;
//			Debug.Log(AcknowledgeActive);
		}
	}

	// COUNTER FOR EXPLOSION CLIPS
	public void BeingPushedActivator()
	{
		BeingPushedActive+=2;
	}

	// REACTIVATES ACKNOWLEDGEMENT
	void FlipGreen()
	{
		AckGreenLight = true;
	}

	// REACTIVES RANDOM PEACE TIME SOUND
	void FlipRandomGreen()
	{
		RandomGreenLight = false;
	}
}
