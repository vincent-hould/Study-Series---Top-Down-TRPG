using UnityEngine;

namespace TopDownTRPG.Assets.Scripts.Framework
{
    public abstract class BaseMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        // Use this for initialization
        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this as T;
        }
    }
}
