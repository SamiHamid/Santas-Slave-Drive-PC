using UnityEngine;

namespace Assets.Scripts
{
    public class Boombox : Target
    {
        private AudioSource _audioSource;
        private Rigidbody _rigidbody;
        private Vector3 _explosionPos;
        [SerializeField]
        private float _force;

        [SerializeField]
        private Jesus _jesus;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        void OnCollisionStay(Collision collision)
        {
            _explosionPos = transform.position + Random.insideUnitSphere * 0.5f;
            _rigidbody.AddExplosionForce(1000.0f, _explosionPos, 5.0f, 0.0f);
        }

        public override string GetTargetType()
        {
            return "Boombox";
        }

        public override void OnLookStart()
        {
            if (GameData.GetCurrentGameData().IsJesusActive)
            {
                SoundManager.GetSharedManager().PlayScratchSound(transform.position);
                _audioSource.mute = true;
                Invoke("ResumeMusic", 3);
            }

            _jesus.CalmDown();
        }

        private void ResumeMusic()
        {
            _audioSource.mute = false;
        }

        public override void OnLookUpdate()
        {

        }

        public override void OnLookEnd()
        {

        }
    }
}
