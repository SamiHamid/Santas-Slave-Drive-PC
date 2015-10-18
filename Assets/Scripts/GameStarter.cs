using UnityEngine;

namespace Assets.Scripts
{
    public class GameStarter : MonoBehaviour
    {
		public GameObject Number1;
		public GameObject Number2;
		public GameObject Number3;
		public GameObject GoObject;
		
		public AudioClip Bell1;
		public AudioClip Bell2;
		
        public float GameStartDelay;

        void OnTriggerEnter(Collider other)
        {
            if (other.transform.name.Equals("CardboardMain") ||
                other.transform.name.Equals("OVR"))
            {
                Invoke("StartGame", GameStartDelay);
                Invoke("No1", 1f);
                Invoke("No2", 2f);
                Invoke("No3", 3f);
                Invoke("Go", 4f);
            }
        }

        private void StartGame()
        {
        	GoObject.SetActive(false);
            GameData.GetCurrentGameData().StartGame();
			
        }
        
        private void No1()
        {
        	Number1.SetActive(true);
			AudioSource.PlayClipAtPoint(Bell1,transform.position, 1.0f);
        }
        
        private void No2()
        {
        	Number1.SetActive(false);
        	Number2.SetActive(true);
			AudioSource.PlayClipAtPoint(Bell1,transform.position, 1.0f);
        }
        
        private void No3()
        {
        	Number2.SetActive(false);
        	Number3.SetActive(true);
			AudioSource.PlayClipAtPoint(Bell1,transform.position, 1.0f);
        }
        
        private void Go()
        {
        	Number3.SetActive(false);
        	GoObject.SetActive(true);
			AudioSource.PlayClipAtPoint(Bell2,transform.position, 1.0f);
        }

    }
}
