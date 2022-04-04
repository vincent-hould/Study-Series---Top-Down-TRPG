
namespace TopDownTRPG
{
    public interface ISelectionZoneHighlighter
    {
        public void Highlight(CursorConstraint cursorConstraint);

        public void RemoveHighlight();
    }
}
