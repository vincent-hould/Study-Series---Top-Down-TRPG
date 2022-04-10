using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Cursor Cursor;

        public ISelectionZoneHighlighter SelectionZoneHighlighter;

        private void Awake()
        {
            SelectionZoneHighlighter = GetComponent<ISelectionZoneHighlighter>();
            SelectionEventChannelSO.OnSelectionRequested += InitCursor;
        }

        private void OnDestroy()
        {
            SelectionEventChannelSO.OnSelectionRequested -= InitCursor;
        }

        public void InitCursor(CursorConstraint cursorConstraint)
        {
            SelectionZoneHighlighter.Highlight(cursorConstraint);
            Cursor.Enable(cursorConstraint);
            Cursor.OnCursorSelection += OnCursorSelection;
        }

        private void OnCursorSelection(Selection selection)
        {
            Cursor.OnCursorSelection -= OnCursorSelection;
            Cursor.Disable();
            SelectionZoneHighlighter.RemoveHighlight();
            SelectionEventChannelSO.RaiseSelectionDone(selection);
        }
    }
}
