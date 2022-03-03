using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField]
        private Cursor Cursor;

        private void Awake()
        {
            SelectionEventChannelSO.OnSelectionRequested += InitCursor;
        }

        public void InitCursor(CursorConstraint cursorConstraint)
        {
            Cursor.Enable(cursorConstraint);
            Cursor.OnCursorSelection += OnCursorSelection;
        }

        private void OnCursorSelection(Selection selection)
        {
            Cursor.OnCursorSelection -= OnCursorSelection;
            Cursor.Disable();
            SelectionEventChannelSO.RaiseSelectionDone(selection);
        }
    }
}
