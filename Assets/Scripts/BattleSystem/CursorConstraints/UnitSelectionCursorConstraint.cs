using UnityEngine;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public class UnitSelectionCursorConstraint : CursorConstraint
    {
        private Faction _faction;

        public UnitSelectionCursorConstraint(Faction faction) : base()
        {
            _faction = faction;
        }

        public override bool CanSelect(Selection selection)
        {
            if (selection.Selectable == null || selection.Selectable.GetType() != typeof(Unit))
                return true;

            Unit target = (Unit) selection.Selectable;
            return target.Faction == _faction;
        }

        public override Vector3 GetNextTile(Vector3 position, Vector3 direction)
        {
            var destination = position + direction;
            return GridManager.Instance.IsInGrid(destination) ? destination : position;
        }

        public override List<Vector3> GetAllowedTiles()
        {
            return GridManager.Instance.GetAllTiles();
        }
    }
}
