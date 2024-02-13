using TopDownTRPG.Assets.Scripts.Framework;
using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionManager : BaseMonoSingleton<SelectionManager>
    {
        [SerializeField] private Cursor Cursor;

        private ISelectionZoneHighlighter _selectionZoneHighlighter;

        protected override void Awake()
        {
            base.Awake();
            _selectionZoneHighlighter = GetComponent<ISelectionZoneHighlighter>();
            BattleEventChannelSO.OnBattleEnded += DisableCursor;
        }

        public void PromptForSelection(CursorConstraint cursorConstraint)
        {
            _selectionZoneHighlighter.Highlight(cursorConstraint);
            Cursor.Show(cursorConstraint, ProcessSelection);
        }

        private void ProcessSelection(Selection selection)
        {
            _selectionZoneHighlighter.RemoveHighlight();
            if (selection != null)
                SelectionEventChannelSO.RaiseSelectionDone(selection);
            else
                SelectionEventChannelSO.RaiseSelectionCancelled();
        }

        private void DisableCursor()
        {
            Cursor.Hide();
        }
    }
}
