using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ChristmasCardUI : MonoBehaviour 
{
	public GameObject CoverHinge;
	private Animator anim;
	
	public GameObject OVRCameraCorrected;
	public GameObject CardboardCamera;
	private Animator CameraAnim;
	private Animator CameraAnimAlp;
	
	public GameObject Instructions1;
	public GameObject Instructions2;
	public GameObject Instructions3;
	public GameObject Instructions4;
	public GameObject Instructions5;
	public GameObject Credits;
	public GameObject CreditsButton;
	public GameObject Back;

	//Audio
	
	private AudioSource AudioS;
	public AudioClip HereWeGo;
	public AudioClip TitleAudio;
	
	// Birds
	public GameObject BirdOAudiobject;
	private AudioSource BirdAudio;
	
	
    void Start () 
	{
		Invoke ("GuiInitialize", 5f);
		anim = CoverHinge.GetComponent<Animator> ();
		CameraAnim = OVRCameraCorrected.GetComponent<Animator> ();
		CameraAnimAlp = CardboardCamera.GetComponent<Animator> ();
		
		AudioS = GetComponent<AudioSource>();
		Invoke ("TitleAudioGo", 3.5f);
	}
	
	void TitleAudioGo()
	{
		
		AudioS.PlayOneShot(TitleAudio, 2f);
	}
	
	void Update () 
	{
	}
	
	// INITIALIZATION
	void GuiInitialize()
	{
		anim.SetTrigger("OpenCard");
	}
	
	// GAME BEGINS
	
	public void GameBegins()
	{
		Invoke("DisableUI", 2f);
		
		AudioS.PlayOneShot(HereWeGo);
		
		if (!OVRCameraCorrected.activeInHierarchy)
		{
			CameraAnimAlp.SetTrigger("OVRSwoop");
			Debug.Log("CARDBOARDCAM CALLED");
		}
		else
		{
			CameraAnim.SetTrigger ("OVRTrigger");
			Debug.Log("OVR CALLED");
		}

       
	}
	
	void DisableUI()
	{
		gameObject.SetActive(false);
	}
	
	// CREDITS
	public void CreditsActive()
	{
		anim.SetTrigger("CloseCard");
		Invoke("CreditsOn", 3.25f);
	}
	
	public void CreditsOn()
	{
		Instructions1.SetActive(false);
		Instructions2.SetActive(false);
		Instructions3.SetActive(false);
		Instructions4.SetActive(false);
		Instructions5.SetActive(false);
		Credits.SetActive(true);
		CreditsButton.SetActive(false);
		Back.SetActive(true);
		anim.SetTrigger("OpenCard");
	}
	
	// CREDITS BACK
	public void BackActive()
	{
		anim.SetTrigger("CloseCard");
		Invoke("BackOn",3.25f);
		Debug.Log ("Closing Card");
	}
	
	public void BackOn()
	{
		Instructions1.SetActive(true);
		Instructions2.SetActive(true);
		Instructions3.SetActive(true);
		Instructions4.SetActive(true);
		Instructions5.SetActive(true);
		Credits.SetActive(false);
		CreditsButton.SetActive(true);
		Back.SetActive(false);
		anim.SetTrigger("OpenCard");
	}
	
	// QUIT
	public void QuitActive()
	{
		anim.SetTrigger("CloseCard");
		Invoke("QuitOn", 3.5f);
	}
	
	void QuitOn()
	{
		Application.Quit();
	}
}
