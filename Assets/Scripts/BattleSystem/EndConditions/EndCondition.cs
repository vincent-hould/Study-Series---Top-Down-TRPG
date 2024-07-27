using UnityEngine;

namespace TopDownTRPG
{
    public abstract class EndCondition : MonoBehaviour
    {
        [SerializeField] public bool IsWin;

        public abstract bool IsConditionMet(BattleStateMachine battleStateMachine);
    }
}
