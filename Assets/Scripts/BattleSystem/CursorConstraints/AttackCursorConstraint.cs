using UnityEngine;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public class AttackCursorConstraint : CursorConstraint
    {
        private Unit _attacker;

        public AttackCursorConstraint(Unit attacker) : base()
        {
            _attacker = attacker;
        }

        public override bool CanSelect(Selection selection)
        {
            if (selection.Selectable == null || selection.Selectable.GetType() != typeof(Unit))
                return false;

            Unit target = (Unit) selection.Selectable;
            return _attacker.Faction.CanAttack(target.Faction);
        }

        public override bool AreAllowedTilesHighlighted()
        {
            return true;
        }

        public override Vector3 GetNextTile(Vector3 position, Vector3 direction)
        {
            var destination = position + direction;
            return GetAllowedTiles().Contains(destination) ? destination: position;
        }

        public override List<Vector3> GetAllowedTiles()
        {
            return GridManager.Instance.GetTilesInRange(_attacker.transform.position, 1, false);
        }

        public override bool CanBeCancelled()
        {
            return true;
        }
    }
}
