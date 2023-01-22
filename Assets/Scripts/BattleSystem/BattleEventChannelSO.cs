using UnityEngine;

namespace TopDownTRPG
{
    public class BattleEventChannelSO : ScriptableObject
    {
        public delegate void UnitRefreshed();
        public static event UnitRefreshed OnUnitRefreshed;
        public static void RaiseUnitRefreshed()
        {
            if (OnUnitRefreshed != null)
                OnUnitRefreshed();
        }

        public delegate void AttackRequested();
        public static event AttackRequested OnAttackRequested;
        public static void RaiseAttackRequested()
        {
            if (OnAttackRequested != null)
                OnAttackRequested();
        }

        public delegate void MoveRequested();
        public static event MoveRequested OnMoveRequested;
        public static void RaiseMoveRequested()
        {
            if (OnMoveRequested != null)
                OnMoveRequested();
        }

        public delegate void EndTurnRequested();
        public static event EndTurnRequested OnEndTurnRequested;
        public static void RaiseEndTurnRequested()
        {
            if (OnEndTurnRequested != null)
                OnEndTurnRequested();
        }

        public delegate void UnitMoveStarted(Transform unitTransform);
        public static event UnitMoveStarted OnUnitMoveStarted;
        public static void RaiseUnitMoveStarted(Transform unitTransform)
        {
            if (OnUnitMoveStarted != null)
                OnUnitMoveStarted(unitTransform);
        }

        public delegate void UnitMoveEnded(Unit unit);
        public static event UnitMoveEnded OnUnitMoveEnded;
        public static void RaiseUnitMoveEnded(Unit unit)
        {
            if (OnUnitMoveEnded != null)
                OnUnitMoveEnded(unit);
        }

        public delegate void UnitSpawned(Unit unit);
        public static event UnitSpawned OnUnitSpawned;
        public static void RaiseUnitSpawned(Unit unit)
        {
            if (OnUnitSpawned != null)
                OnUnitSpawned(unit);
        }

        public delegate void UnitKilled(Unit unit);
        public static event UnitKilled OnUnitKilled;
        public static void RaiseUnitKilled(Unit unit)
        {
            if (OnUnitKilled != null)
                OnUnitKilled(unit);
        }
    }
}
