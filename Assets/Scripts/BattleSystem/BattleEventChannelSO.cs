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

        public delegate void UnitMoveEnded();
        public static event UnitMoveEnded OnUnitMoveEnded;
        public static void RaiseUnitMoveEnded()
        {
            if (OnUnitMoveEnded != null)
                OnUnitMoveEnded();
        }
    }
}
