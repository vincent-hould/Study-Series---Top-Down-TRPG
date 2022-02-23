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

        public void InitCursor()
        {
            Cursor.SetActive(true);
            Cursor.OnCursorSelection += OnCursorSelection;
        }

        private void OnCursorSelection(Selection selection)
        {
            Cursor.OnCursorSelection -= OnCursorSelection;
            Cursor.SetActive(false);
            SelectionEventChannelSO.RaiseSelectionDone(selection);
        }
    }
}
