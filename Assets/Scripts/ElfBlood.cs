using UnityEngine;

namespace Assets.Scripts
{
    public class ElfBlood : MonoBehaviour {

        public GameObject[] candyArray;
        public Vector3 ExplosionPosition;
        int numCandy = 10;

        // Use this for initialization
        void Start ()
        {
            GameObject factory = GameObject.Find("Blood And Gore Factory");
            for (int i = 0; i < numCandy; i++) {
                int rand = Random.Range(0,candyArray.Length);
                Vector3 loc = Random.insideUnitSphere;
                Quaternion rot = Random.rotation;
                GameObject instance = Instantiate(candyArray[rand], loc + transform.position, rot) as GameObject;
                Rigidbody rb = instance.GetComponent<Rigidbody>();
                rb.AddExplosionForce(10, transform.position, 5, 3.0F);

                instance.transform.parent = factory.transform;
            }
            Destroy(gameObject, 3f);
        }
    }
}
