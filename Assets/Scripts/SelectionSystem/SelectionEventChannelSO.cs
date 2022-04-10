using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionEventChannelSO : ScriptableObject
    {
        public delegate void SelectionRequested(CursorConstraint cursorConstraint);
        public static event SelectionRequested OnSelectionRequested;
        public delegate void SelectionDone(Selection selection);
        public static event SelectionDone OnSelectionDone;
        public delegate void SelectionCancelled();
        public static event SelectionCancelled OnSelectionCancelled;

        public static void RaiseSelectionRequest(CursorConstraint cursorConstraint = null)
        {
            if (OnSelectionRequested != null)
                OnSelectionRequested(cursorConstraint);
        }

        public static void RaiseSelectionDone(Selection selection)
        {
            if (OnSelectionDone != null)
                OnSelectionDone(selection);
        }

        public static void RaiseSelectionCancelled()
        {
            if (OnSelectionCancelled != null)
            {
                OnSelectionCancelled();
            }
        }
    }
}
