using UnityEngine;

namespace TopDownTRPG
{
    [CreateAssetMenu(fileName = "NewUnitType", menuName = "Create a unit type")]
    public class UnitType : ScriptableObject
    {
        public string Name;
        public int MovementRange;
        public int MaxHealth;
        public int Damage;

        public CursorConstraint GetAttackCursorConstraint(Unit unit)
        {
            return new AttackCursorConstraint(unit);
        }

        public CursorConstraint GetMoveCursorConstraint(Unit unit)
        {
            return new MoveCursorConstraint(unit);
        }
    }
}
