using UnityEngine;
using System.Collections;

public class GUIRayCasting : MonoBehaviour {

	// RayCasting
	public bool GUIRay = false;

	// Christmas Card Script
	public GameObject ChristmasCard;
	private ChristmasCardUI CardScript;

	// Button Stars
	public GameObject QuitL;
	public GameObject QuitR;
	
	public GameObject CreditL;
	public GameObject CreditR;

	public GameObject BackL;
	public GameObject BackR;

	// Boolean Flips
	private int StartFlip;
	private bool CreditFlip = true;
	private bool QuitFlip = true;
	private bool BackFlip = true;

	// Start Image
	public GameObject FirstGroup;
	public GameObject SecondGroup;
	public GameObject ThirdGroup;
	public GameObject FourthGroup;
	public GameObject FifthGroup;

	void Start () 
	{
		CardScript = ChristmasCard.GetComponent<ChristmasCardUI> ();
	}

	public void SetRay()
	{
		GUIRay = false;
	}

	void Update () 
	{

		if (GUIRay) 
		{
			Ray GRay = new Ray (transform.position,transform.forward);
			RaycastHit hit;

			if (Physics.Raycast(GRay, out hit, 100))
		   	{
				Debug.DrawRay(transform.position, transform.forward, Color.green, 1);

				if (hit.collider.CompareTag("Start"))
				{
					StartFlip +=1;

					if (StartFlip == 20)
					{
						FirstGroup.SetActive(true);
						GetComponent<AudioSource>().Play();
					}

					else if (StartFlip == 30)
					{
						SecondGroup.SetActive(true);
						GetComponent<AudioSource>().Play();
					}
					else if (StartFlip == 40)
					{
						ThirdGroup.SetActive(true);
						GetComponent<AudioSource>().Play();
					}
					else if (StartFlip == 50)
					{
						FourthGroup.SetActive(true);
						GetComponent<AudioSource>().Play();

					} 
					else if (StartFlip == 60)
					{
						FifthGroup.SetActive(true);
						GetComponent<AudioSource>().Play();
						CardScript.GameBegins();
						Invoke("SetRay", 3);
					}
				}
				if (!hit.collider.CompareTag("Start"))
				   	{

					}

				if (hit.collider.CompareTag("Credits"))
				{
					if (CreditFlip == true)
					{
						CardScript.CreditsActive();
						CreditL.SetActive(true);
						CreditR.SetActive(true);
						CreditFlip = false;
						BackFlip = true;
					}
				}

				if (hit.collider.CompareTag("Quit"))
				{
					if (QuitFlip == true)
					{
						CardScript.QuitActive();
						QuitL.SetActive(true);
						QuitR.SetActive(true);
						QuitFlip = false;
					}
				}

				if (hit.collider.CompareTag("CreditsBack"))
				{
					if (BackFlip == true)
					{
						CardScript.BackActive();
						BackL.SetActive(true);
						BackR.SetActive(true);
						BackFlip = false;
						CreditFlip = true;
					}
				}
			}
		}
	}
}
