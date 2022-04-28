using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionEventChannelSO : ScriptableObject
    {
        public delegate void SelectionRequested(CursorConstraint cursorConstraint);
        public static event SelectionRequested OnSelectionRequested;
        public static void RaiseSelectionRequest(CursorConstraint cursorConstraint = null)
        {
            if (OnSelectionRequested != null)
                OnSelectionRequested(cursorConstraint);
        }

        public delegate void SelectionDone(Selection selection);
        public static event SelectionDone OnSelectionDone;
        public static void RaiseSelectionDone(Selection selection)
        {
            if (OnSelectionDone != null)
                OnSelectionDone(selection);
        }

        public delegate void SelectionCancelled();
        public static event SelectionCancelled OnSelectionCancelled;
        public static void RaiseSelectionCancelled()
        {
            if (OnSelectionCancelled != null)
            {
                OnSelectionCancelled();
            }
        }

        public delegate void CursorEnabled(Transform cursorTransform);
        public static event CursorEnabled OnCursorEnabled;
        public static void RaiseCursorEnabled(Transform cursorTransform)
        {
            if (OnCursorEnabled != null)
            {
                OnCursorEnabled(cursorTransform);
            }
        }

        public delegate void CursorDisabled();
        public static event CursorDisabled OnCursorDisabled;
        public static void RaiseCursorDisabled()
        {
            if (OnCursorDisabled != null)
            {
                OnCursorDisabled();
            }
        }
    }
}
