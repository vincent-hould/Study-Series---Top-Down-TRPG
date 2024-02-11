using TopDownTRPG.Assets.Scripts.Framework;
using UnityEngine;

namespace TopDownTRPG
{
    public class SelectionManager : BaseMonoSingleton<SelectionManager>
    {
        [SerializeField] private Cursor Cursor;

        public ISelectionZoneHighlighter SelectionZoneHighlighter;

        protected override void Awake()
        {
            base.Awake();
            SelectionZoneHighlighter = GetComponent<ISelectionZoneHighlighter>();
            BattleEventChannelSO.OnBattleEnded += DisableCursor;
        }

        public void Select(Selection selection)
        {
            SelectionZoneHighlighter.RemoveHighlight();
            if (selection != null)
                SelectionEventChannelSO.RaiseSelectionDone(selection);
            else
                SelectionEventChannelSO.RaiseSelectionCancelled();
        }

        public void PromptForSelection(CursorConstraint cursorConstraint)
        {
            SelectionZoneHighlighter.Highlight(cursorConstraint);
            Cursor.Show(this, cursorConstraint);
        }

        private void DisableCursor()
        {
            Cursor.Hide();
        }
    }
}
