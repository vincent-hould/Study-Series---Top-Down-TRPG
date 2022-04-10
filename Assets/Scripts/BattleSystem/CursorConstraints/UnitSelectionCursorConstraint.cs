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

        public override List<Vector3> GetAllowedTiles()
        {
            return new List<Vector3>();
        }
    }
}
