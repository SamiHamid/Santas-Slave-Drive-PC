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
        [SerializeField] private TextMesh RemainingTimeGUI;

        public bool IsJesusActive;
	    public bool IsGameStarted;

		private static GameData _currentGameData;
		
		public GameObject Boombox;
		
		//ENDING UI
		public GameObject WinLoseUI;
		public GameObject Win;
		public GameObject Lose;
		private Animator anim;
		
		//GUI
		public GameObject GUIBoard;


        [SerializeField]
        private GameObject _characters;

        void Start () {
			_currentGameData = this;
		    _currentRequiredPresentCount = 60;
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

	    private void UpdateRemainingTimeGUI()
	    {
	        float remainingTime = _givenTimeOnThisLevel - (Time.time - _levelStartTime);
            TimeSpan timeSpan = TimeSpan.FromSeconds(remainingTime);
	        RemainingTimeGUI.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }

	    public void ResetGameData()
		{
			_currentLevel = 0;
			_currentPresentCount = 0;
            _remainingElfCount = GameObject.Find("Elves").transform.childCount;
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

        // not used
		public void IncrementLevel()
		{
			_currentLevel++;
			_currentPresentCount = 0;
			UpdateYearGUI();
		}

		private void UpdateYearGUI()
		{
			YearGUI.text = string.Format ("{0,2}", _currentLevel+1);
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
        {
            _characters.SetActive(true);
            RandomizeElves();
            IsGameStarted = true;
		    _levelStartTime = Time.time;
			ResetGameData();
			GUIBoard.SetActive(true);
			Boombox.SetActive(true);
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
            for (int i = 0; i < childCount; i++)
	        {
	            int randomPositionIndex = randomPositionIndices[i];
	            randomElfPoint = elfSpawnPoints.GetChild(randomPositionIndex);
	            currentElf = elves.GetChild(i);
	            currentElf.transform.position = randomElfPoint.position;
	            currentElf.transform.rotation = randomElfPoint.rotation;
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
	    }
	    
	    void RestartLevel()
	    {
	    	Application.LoadLevel(Application.loadedLevel);	
	    }
	}
}