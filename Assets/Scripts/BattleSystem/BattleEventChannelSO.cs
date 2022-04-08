using UnityEngine;

namespace TopDownTRPG
{
    public class BattleEventChannelSO : ScriptableObject
    {
        public delegate void UnitRefreshed();
        public static event UnitRefreshed OnUnitRefreshed;

        public delegate void AttackRequested();
        public static event AttackRequested OnAttackRequested;

        public delegate void MoveRequested();
        public static event MoveRequested OnMoveRequested;

        public delegate void EndTurnRequested();
        public static event EndTurnRequested OnEndTurnRequested;

        public static void RaiseUnitRefreshed()
        {
            if (OnUnitRefreshed != null)
            {
                OnUnitRefreshed();
            }
        }

        public static void RaiseAttackRequested()
        {
            if (OnAttackRequested != null)
            {
                OnAttackRequested();
            }
        }

        public static void RaiseMoveRequested()
        {
            if (OnMoveRequested != null)
            {
                OnMoveRequested();
            }
        }

        public static void RaiseEndTurnRequested()
        {
            if (OnEndTurnRequested != null)
            {
                OnEndTurnRequested();
            }
        }
    }
}
