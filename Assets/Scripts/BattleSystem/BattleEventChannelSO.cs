using UnityEngine;

namespace TopDownTRPG
{
    public class BattleEventChannelSO : ScriptableObject
    {
        public delegate void UnitRefreshed();
        public static event UnitRefreshed OnUnitRefreshed;
        public static void RaiseUnitRefreshed()
        {
            OnUnitRefreshed?.Invoke();
        }

        public delegate void UnitMoveStarted(Transform unitTransform);
        public static event UnitMoveStarted OnUnitMoveStarted;
        public static void RaiseUnitMoveStarted(Transform unitTransform)
        {
            OnUnitMoveStarted?.Invoke(unitTransform);
        }

        public delegate void UnitMoveEnded(Unit unit);
        public static event UnitMoveEnded OnUnitMoveEnded;
        public static void RaiseUnitMoveEnded(Unit unit)
        {
            OnUnitMoveEnded?.Invoke(unit);
        }

        public delegate void UnitSpawned(Unit unit);
        public static event UnitSpawned OnUnitSpawned;
        public static void RaiseUnitSpawned(Unit unit)
        {
            OnUnitSpawned?.Invoke(unit);
        }

        public delegate void UnitKilled(Unit unit);
        public static event UnitKilled OnUnitKilled;
        public static void RaiseUnitKilled(Unit unit)
        {
            OnUnitKilled?.Invoke(unit);
        }

        public delegate void BattleEnded();
        public static event BattleEnded OnBattleEnded;
        public static void RaiseBattleEnded()
        {
            OnBattleEnded?.Invoke();
        }
    }
}
