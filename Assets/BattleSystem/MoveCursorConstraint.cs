
namespace TopDownTRPG
{
    public class MoveCursorConstraint : CursorConstraint
    {
        public override bool CanSelect(Selection selection)
        {
            return selection.Selectable == null;
        }
    }
}
