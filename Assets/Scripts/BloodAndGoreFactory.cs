using UnityEngine;

namespace Assets.Scripts
{
    public class BloodAndGoreFactory : MonoBehaviour
    {

        [SerializeField] private GameObject _elfBloodAndGore;

        public GameObject CreateBloodAndGore(Vector3 pos)
        {
            pos.y += 3;
			GameObject bloodAndGoreParticles = Instantiate(_elfBloodAndGore, pos, Quaternion.identity) as GameObject;
            return bloodAndGoreParticles;
        }

    }
}
