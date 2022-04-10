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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.Disable();
                Select(null);
            }
        }

        private void OnDestroy()
        {
            SelectionEventChannelSO.OnSelectionRequested -= InitCursor;
        }

        public void Select(Selection selection)
        {
            SelectionZoneHighlighter.RemoveHighlight();
            if (selection != null)
                SelectionEventChannelSO.RaiseSelectionDone(selection);
            else
                SelectionEventChannelSO.RaiseSelectionCancelled();
        }

        private void InitCursor(CursorConstraint cursorConstraint)
        {
            SelectionZoneHighlighter.Highlight(cursorConstraint);
            Cursor.Init(this, cursorConstraint);
        }
    }
}
