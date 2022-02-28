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
            {
                OnUnitRefreshed();
            }
        }
    }
}
