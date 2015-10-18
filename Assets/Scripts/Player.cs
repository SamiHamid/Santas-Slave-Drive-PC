using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : MonoBehaviour
    {
        private GameObject _lastTarget;

        void Update ()
        {
            if (!GameData.GetCurrentGameData().IsGameStarted)
                return;

			RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(transform.position, fwd, out hit, 60))
            {
                GameObject hitObject = hit.transform.gameObject;
                Target hitObjectTarget = hitObject.GetComponent<Target>();
                if (hitObjectTarget != null)
                {
                    if (_lastTarget == null)
                    {
                        _lastTarget = hitObject;
                        hitObjectTarget.OnLookStart();
                    }
                    else
                    {
                        if (_lastTarget != hitObject)
                        {
                            Target lastTarget = _lastTarget.GetComponent<Target>();
                            lastTarget.OnLookEnd();
                            _lastTarget = hitObject;
                            hitObjectTarget.OnLookStart();
                        }
                    }
                    
                }
                else
                {
                    if (_lastTarget != null)
                    {
                        Target t = _lastTarget.GetComponent<Target>();
                        t.OnLookEnd();
                        _lastTarget = null;
                    }
                }
            }
            else
            {
                if (_lastTarget != null)
                {
                    Target t = _lastTarget.GetComponent<Target>();
                    t.OnLookEnd();
                    _lastTarget = null;
                }
            }

        }
    }
}
