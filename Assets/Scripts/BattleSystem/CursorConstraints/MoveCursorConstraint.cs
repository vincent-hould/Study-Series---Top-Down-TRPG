using UnityEngine;

namespace TopDownTRPG
{
    public class MoveCursorConstraint : CursorConstraint
    {
        private Unit _movedUnit;

        public MoveCursorConstraint(Unit unit) : base()
        {
            _movedUnit = unit;
        }

        public override bool CanSelect(Selection selection)
        {
            return selection.Selectable == null &&
                   Vector3.Distance(selection.Position, _movedUnit.transform.position) <= _movedUnit.MovementRange;
        }
    }
}
