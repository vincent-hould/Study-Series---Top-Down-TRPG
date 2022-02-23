using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionEventChannelSO : ScriptableObject
    {
        public delegate void SelectionRequested();
        public static event SelectionRequested OnSelectionRequested;
        public delegate void SelectionDone(Selection selection);
        public static event SelectionDone OnSelectionDone;

        public static void RaiseSelectionRequest()
        {
            if (OnSelectionRequested != null)
            {
                OnSelectionRequested();
            }
        }

        public static void RaiseSelectionDone(Selection selection)
        {
            if (OnSelectionDone != null)
            {
                OnSelectionDone(selection);
            }
        }
    }
}
