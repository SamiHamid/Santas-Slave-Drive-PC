using System;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Target : MonoBehaviour
    {
        public abstract String GetTargetType();
        public abstract void OnLookStart();
        public abstract void OnLookUpdate();
        public abstract void OnLookEnd();
    }
}
