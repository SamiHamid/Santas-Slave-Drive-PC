using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{

    public class LaserBeams : MonoBehaviour
    {
        private ArrayList _targets;
        Quaternion _targetRotation;

        private Jesus _jesus;

        [SerializeField] private float _currentSpeed;
        [SerializeField] private float _initialSpeed;
        [SerializeField] private float _incrementalSpeed;
        [SerializeField] private float _lockOnTime;

        private bool _isVisible;
        private bool IsVisible
        {
            set
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _isVisible = value;
                if (_isVisible) _isLockedOnTarget = false;
            }
            get { return _isVisible; }
        }

        private bool _isLockedOnTarget;
        private float _lockOnStartTime;

        public Quaternion TargetRotation
        {
            get { return _targetRotation; }
            set { _targetRotation = value; }
        }

        void Start ()
        {
            _jesus = transform.parent.parent.GetComponent<Jesus>();

            IsVisible = true;
            _targetRotation = _jesus.PickNewTarget();
        }
	
        void Update ()
        {
            if (IsVisible)
            {
                if (!_isLockedOnTarget)
                {
                    if (Quaternion.Angle(transform.rotation, _targetRotation) < 0.1f)
                    {
                        _lockOnStartTime = Time.time;
                        _isLockedOnTarget = true;
                    }
                    else
                    {
                        float angle = Quaternion.Angle(transform.rotation, _targetRotation);
                        float timeToComplete = angle/_currentSpeed;
                        float donePercentage = Mathf.Min(1F, Time.deltaTime/timeToComplete);

                        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, donePercentage);
                    }
                }
                else
                {
                    if (Time.time - _lockOnStartTime > _lockOnTime)
                    {
                        _isLockedOnTarget = false;
                        _targetRotation = _jesus.PickNewTarget();
                        _currentSpeed += _incrementalSpeed;
                    }
                }
            }
        }

        public void SetSpeedBackToMin()
        {
            _currentSpeed = _initialSpeed;
        }
    }
}
