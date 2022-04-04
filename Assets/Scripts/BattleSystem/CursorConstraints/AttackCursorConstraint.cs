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
            if (selection.Selectable == null || selection.Selectable.GetType() != typeof(Unit)) return false;

            Unit target = (Unit) selection.Selectable;
            return _attacker.Faction.CanAttack(target.Faction);
        }

        public override Vector3 GetNextTile(Vector3 position, Vector3 direction)
        {
            var destination = position + direction;
            var distance = Vector3.Distance(_attacker.transform.position, destination);
            return distance <= 1 ? destination : position;
        }

        public override List<Vector3> GetAllowedTiles()
        {
            var tiles = new List<Vector3>();
            Vector3 attackerPosition = _attacker.transform.position;
            tiles.Add(attackerPosition + Vector3.up);
            tiles.Add(attackerPosition + Vector3.down);
            tiles.Add(attackerPosition + Vector3.left);
            tiles.Add(attackerPosition + Vector3.right);
            return tiles;
        }
    }
}
