using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GiftFactory : MonoBehaviour {

        public Transform GiftPrefab;
        public Transform SmallGiftPrefab;
        public Transform SmallGiftParticleEffect;
        public Transform SmallGiftSpawnPosition;

        private Transform _giftContainer;
        private Transform _smallGiftContainer;

        void Start()
        {
            _giftContainer = transform.FindChild("Gifts");
            _smallGiftContainer = transform.FindChild("Small Gifts");
        }

        public Transform CreateGift(Vector3 pos)
        {
            Vector3 loc = Random.insideUnitSphere;
            loc.y += 2;
            Quaternion rot = Random.rotation;
            Transform gift = Instantiate(GiftPrefab, pos, rot) as Transform;
            if (gift != null)
            {
                Rigidbody rb = gift.GetComponent<Rigidbody>();
                rb.AddExplosionForce(500, pos + loc, 100, 3.0F);
                Gift giftComponent = gift.GetComponent<Gift>();
                giftComponent.GiftFactory = this;
                gift.parent = _giftContainer;
            }
            return gift;
        }

        public Transform CreateSmallGift(Vector3 particlePos)
        {
            Instantiate(SmallGiftParticleEffect, particlePos, Quaternion.identity);
            Vector3 spawnPos = SmallGiftSpawnPosition.position;
            spawnPos.x = spawnPos.x + Random.Range(-2.0f, 2.0f);
            spawnPos.z = spawnPos.z + Random.Range(-2.0f, 2.0f);
            Transform smallGift = Instantiate(SmallGiftPrefab, spawnPos, Quaternion.identity) as Transform;
            if (smallGift != null)
            {
                smallGift.parent = _smallGiftContainer;
            }
            return smallGift;
        }
    }
}
