using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public interface IJesusDelegate
    {
        void OnElfDestroyed(Transform elf);
    }

    public class Jesus : MonoBehaviour, IJesusDelegate
    {
        [SerializeField] private Transform[] _spawnPointTransforms;
        private Transform _jesusParts;
        private Transform _laserBeams;
        private LaserBeams _laserBeamsComponent;

        private ArrayList _targets;

        private float _nextSpawnTime;
        [SerializeField] private float _stayOnSamePointTime;

        [Space(10), SerializeField] private float _minSpawnTime;
        [Space(10), SerializeField] private float _maxSpawnTime;

        private bool _isAnnihilating;

        void Start()
        {
            _jesusParts = transform.FindChild("Jesus Parts Container");
            _laserBeams = _jesusParts.FindChild("Laser Beam Base");
            _laserBeamsComponent = _laserBeams.GetComponent<LaserBeams>();

            Transform tmp = transform.parent.GetChild(0); //bad programming
            _targets = new ArrayList();
            for (int i = 0; i < tmp.childCount; i++)
            {
                Transform t = tmp.GetChild(i);
                t.GetComponent<Elf>().JesusLookTarget = _jesusParts.FindChild("eye - left");
                _targets.Add(tmp.GetChild(i));
            }

            DetermineNextSpawnTime();
        }

        private float _timeSinceLastMovement;

        private void FixedUpdate()
        {
            if (Time.time > _nextSpawnTime)
            {
                FuckShitUp();
            }

            if (_isAnnihilating)
            {
                if (_timeSinceLastMovement > _stayOnSamePointTime)
                {
                    _timeSinceLastMovement = 0;
                    MoveToNewPoint();
                }
                _timeSinceLastMovement += Time.deltaTime;
            }
        }

        private void MoveToNewPoint()
        {
            int randomPointIndex = Random.Range(0, _spawnPointTransforms.Length - 1);
            Transform randomPoint = _spawnPointTransforms[randomPointIndex];
            transform.position = randomPoint.position;
            transform.rotation = randomPoint.rotation;

            _laserBeams.localRotation = Quaternion.Euler(0, 180, 0);
            _laserBeamsComponent.TargetRotation = PickNewTarget();
        }

        public void OnElfDestroyed(Transform elf)
        {
            _targets.Remove(elf);
        }

        public Quaternion PickNewTarget()
        {
            if (_targets != null &&_targets.Count > 0)
            {
                int newTargetIndex = Random.Range(0, _targets.Count);
                return Quaternion.LookRotation(((Transform)_targets[newTargetIndex]).position - _laserBeams.position);
            }

            return Quaternion.identity;
        }

        public void CalmDown()
        {
            if (_isAnnihilating)
            {
                _isAnnihilating = false;
                GameData.GetCurrentGameData().IsJesusActive = _isAnnihilating;
                _timeSinceLastMovement = 0;
                _jesusParts.gameObject.SetActive(false);
                DetermineNextSpawnTime();
                _laserBeamsComponent.SetSpeedBackToMin();

				foreach (Transform t in _targets)
				{
					Elf elf = t.GetComponent<Elf>();
					elf.StopWorking();
                }
                GameData.GetCurrentGameData().IsJesusActive = false;
				GetComponent<AudioSource>().Stop();
            }
        }

        public void FuckShitUp()
        {
			GetComponent<AudioSource>().Play();
            GameData.GetCurrentGameData().IsJesusActive = true;
            _nextSpawnTime = Mathf.Infinity;
            _isAnnihilating = true;
            GameData.GetCurrentGameData().IsJesusActive = _isAnnihilating;

            _jesusParts.gameObject.SetActive(true);
            MoveToNewPoint();
        }

        private void DetermineNextSpawnTime()
        {
            _nextSpawnTime = Time.time + Random.Range(_minSpawnTime, _maxSpawnTime);
        }
    }
}
