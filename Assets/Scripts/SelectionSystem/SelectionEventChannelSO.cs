using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionEventChannelSO : ScriptableObject
    {
        public delegate void SelectionDone(Selection selection);
        public static event SelectionDone OnSelectionDone;
        public static void RaiseSelectionDone(Selection selection)
        {
            OnSelectionDone?.Invoke(selection);
        }

        public delegate void SelectionCancelled();
        public static event SelectionCancelled OnSelectionCancelled;
        public static void RaiseSelectionCancelled()
        {
            OnSelectionCancelled?.Invoke();
        }

        public delegate void CursorEnabled(Transform cursorTransform);
        public static event CursorEnabled OnCursorEnabled;
        public static void RaiseCursorEnabled(Transform cursorTransform)
        {
            OnCursorEnabled?.Invoke(cursorTransform);
        }

        public delegate void CursorDisabled();
        public static event CursorDisabled OnCursorDisabled;
        public static void RaiseCursorDisabled()
        {
            OnCursorDisabled?.Invoke();
        }
    }
}
