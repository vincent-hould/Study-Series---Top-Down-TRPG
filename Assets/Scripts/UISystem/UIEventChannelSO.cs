using UnityEngine;

namespace TopDownTRPG
{
    public class UIEventChannelSO : ScriptableObject
    {
        public delegate void ActionMenuCancelled();
        public static event ActionMenuCancelled OnActionMenuCancelled;
        public static void RaiseActionMenuCancelled()
        {
            OnActionMenuCancelled?.Invoke();
        }
    }
}
