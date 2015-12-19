using System;
using UnityEngine;
using System.Collections;
using Random = System.Random;

namespace Assets.Scripts
{
	public class GameData : MonoBehaviour {

		private int _remainingElfCount;

		private int _currentLevel;
		private int _currentPresentCount;
		private int _currentRequiredPresentCount;
		private float _givenTimeOnThisLevel;
		private float _levelStartTime;

		[SerializeField] private TextMesh YearGUI;
		[SerializeField] private TextMesh PresentCountGUI;
		[SerializeField] private TextMesh PresentRequiredGUI;
		[SerializeField] private TextMesh RemainingTimeGUI;

		public bool IsJesusActive;
		public bool IsGameStarted;

		private static GameData _currentGameData;

		public GameObject Boombox;

		private int[] giftGoal = new int[10] {5,10,20,30,40,50,60,70,80,99};
		private int[] numberElvesThisLevel = new int[10] {2,3,4,5,6,7,8,9,10,10};


		//ENDING UI
		public GameObject WinLoseUI;
		public GameObject Win;
		public GameObject Lose;
		private Animator anim;

		//GUI
		public GameObject GUIBoard;


		[SerializeField]
		private GameObject _characters;

		private SavedData savedData;

		void Start () {
			savedData = GameObject.Find("SavedData").GetComponent<SavedData>();
			_currentGameData = this;
			//_currentRequiredPresentCount = giftGoal[savedData.currentLevel];
			//PresentRequiredGUI.text = " /" + giftGoal[savedData.currentLevel].ToString(); //moved to StartGame
			_givenTimeOnThisLevel = 60;
		}

		void FixedUpdate()
		{
			if (IsGameStarted)
			{
				if (Time.time - _levelStartTime > _givenTimeOnThisLevel)
				{
					OnLevelFailed();
				}
				UpdateRemainingTimeGUI();


			}
		}

		private void UpdateRemainingTimeGUI()  // called in FixedUpdate(), just leave it alone.
		{
			float remainingTime = _givenTimeOnThisLevel - (Time.time - _levelStartTime);
			TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
			RemainingTimeGUI.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		}

		public void ResetGameData()
		{
			//_currentLevel = 0; // old method
			savedData.currentLevel = 0;
			_currentPresentCount = 0;
			//_remainingElfCount = GameObject.Find("Elves").transform.childCount;
			UpdateYearGUI ();
		}

		public void IncrementPresentCount()
		{
			_currentPresentCount++;
			if (_currentPresentCount >= _currentRequiredPresentCount)
			{
				OnLevelCompleted();
			}
			UpdatePresentCountGUI();
		}

		public void DecrementRemainingElfCount()
		{
			if (IsGameStarted)
			{
				_remainingElfCount--;
				Debug.Log("Remaining Elf Count: " + _remainingElfCount);
				if (_remainingElfCount <= 0)
				{
					OnLevelFailed();
				}
			}
		}


		public void IncrementLevel() 
		// called in OnLevelCompleted()
		{
			//_currentLevel++;
			savedData.currentLevel++;
			//_currentPresentCount = 0;
			//UpdateYearGUI(); // was causing year GUI to update immediately upon winning
			//_currentRequiredPresentCount = giftGoal[savedData.currentLevel];
			//PresentRequiredGUI.text = " /" + giftGoal[savedData.currentLevel].ToString();
		}

		private void UpdateYearGUI()
		{
			//YearGUI.text = string.Format ("{0,2}", _currentLevel+1);
			YearGUI.text = string.Format ("{0,2}", savedData.currentLevel+1);
			UpdatePresentCountGUI();
		}

		private void UpdatePresentCountGUI()
		{
			PresentCountGUI.text = string.Format ("{0}", _currentPresentCount);
		}

		public static GameData GetCurrentGameData()
		{
			return _currentGameData;
		}

		public void StartGame() 
		//called by GameStart.cs after camera has entered grotto.
		{
			_characters.SetActive(true);
			RandomizeElves();
			IsGameStarted = true;
			_levelStartTime = Time.time;
			//ResetGameData();				// resets everything back to Level 1
			GUIBoard.SetActive(true);
			Boombox.SetActive(true);
			_currentPresentCount = 0;
			_currentRequiredPresentCount = giftGoal[savedData.currentLevel];
			PresentRequiredGUI.text = " /" + giftGoal[savedData.currentLevel].ToString();
			_currentRequiredPresentCount = giftGoal[savedData.currentLevel];
			UpdateYearGUI();
			//_remainingElfCount = GameObject.Find("Elves").transform.childCount;
			_remainingElfCount = numberElvesThisLevel[savedData.currentLevel];

		}

		private void RandomizeElves()
		{
			Transform elfSpawnPoints = GameObject.Find("Elf Spawn Points").transform;
			int possibleNoOfSpawnPoints = elfSpawnPoints.childCount;
			int[] randomPositionIndices = new int[possibleNoOfSpawnPoints];
			for (int i = 0; i < possibleNoOfSpawnPoints; i++)
			{
				randomPositionIndices[i] = i;
			}

			randomPositionIndices = shuffleArray(randomPositionIndices);

			Transform elves = _characters.transform.FindChild("Elves");
			int childCount = elves.childCount;
			Transform currentElf, randomElfPoint;
			for (int i = 0; i < numberElvesThisLevel[savedData.currentLevel]; i++)   // was childCount
			{
				int randomPositionIndex = randomPositionIndices[i];
				randomElfPoint = elfSpawnPoints.GetChild(randomPositionIndex);
				currentElf = elves.GetChild(i);
				currentElf.transform.position = randomElfPoint.position;
				currentElf.transform.rotation = randomElfPoint.rotation;
			}

			for (int i = numberElvesThisLevel[savedData.currentLevel]; i < childCount; i++) 
			{
				currentElf = elves.GetChild(i);
				currentElf.gameObject.SetActive(false);
			}
		}

		private int[] shuffleArray(int[] array)
		{
			Random r = new Random();
			for (int i = array.Length; i > 0; i--)
			{
				int j = r.Next(i);
				int k = array[j];
				array[j] = array[i - 1];
				array[i - 1] = k;
			}
			return array;
		}

		private void OnLevelCompleted()
		{
			IsGameStarted = false;
			anim = WinLoseUI.GetComponent<Animator> ();
			anim.SetTrigger ("Go");
			Win.SetActive (true);
			Lose.SetActive (false);
			Debug.Log("LEVEL COMPLETE");
			GetComponent<AudioSource>().Play();
			Invoke("RestartLevel",10);
			IncrementLevel();
		}

		private void OnLevelFailed()
		{
			IsGameStarted = false;
			anim = WinLoseUI.GetComponent<Animator> ();
			anim.SetTrigger ("Go");
			Win.SetActive (false);
			Lose.SetActive (true);
			Debug.Log("LEVEL FAILED");
			GetComponent<AudioSource>().Play();
			Invoke("RestartLevel",10);
			savedData.currentLevel = 0; // reset the game to 1st level

		}

		void RestartLevel()
		{
			Application.LoadLevel(Application.loadedLevel);	
		}
	}
}