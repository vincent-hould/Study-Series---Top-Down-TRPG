using UnityEngine;
using System.Collections.Generic;

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
                GridManager.Instance.IsWalkable(selection.Position) &&
                Vector3.Distance(selection.Position, _movedUnit.transform.position) <= _movedUnit.MovementRange;
        }

        public override bool AreAllowedTilesHighlighted()
        {
            return true;
        }

        public override Vector3 GetNextTile(Vector3 position, Vector3 direction)
        {
            var destination = position + direction;
            // It is by design that we can move through non-walkables. This is to make navigation easier, we just won't be able to select those tiles
            return GridManager.Instance.IsInGrid(destination) ? destination : position;
        }

        public override List<Vector3> GetAllowedTiles()
        {
            return GridManager.Instance.GetTilesInRange(_movedUnit.transform.position, _movedUnit.MovementRange, true);
        }

        public override bool CanBeCancelled()
        {
            return true;
        }
    }
}
