using UnityEngine;

namespace TopDownTRPG
{
    public class UIEventChannelSO : ScriptableObject
    {
        public delegate void ContextualMenuDisplayRequested(Vector3 position);
        public static event ContextualMenuDisplayRequested OnContextualMenuDisplayRequested;

        public delegate void ContextualMenuHideRequested();
        public static event ContextualMenuHideRequested OnContextualMenuHideRequested;

        public delegate void HeaderTextRequested(string text, float duration);
        public static event HeaderTextRequested OnHeaderTextRequested;

        public static void RaiseContextualMenuDisplayRequest(Vector3 position)
        {
            if (OnContextualMenuDisplayRequested != null)
            {
                OnContextualMenuDisplayRequested(position);
            }
        }

        public static void RaiseContextualMenuHideRequest()
        {
            if (OnContextualMenuHideRequested != null)
            {
                OnContextualMenuHideRequested();
            }
        }

        public static void RaiseHeaderTextRequest(string text, float duration)
        {
            if (OnHeaderTextRequested != null)
            {
                OnHeaderTextRequested(text, duration);
            }
        }
    }
}
