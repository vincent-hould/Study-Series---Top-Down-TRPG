using UnityEngine;

namespace TopDownTRPG
{
    public class UnitDeadEndCondition : EndCondition
    {
        [SerializeField] Unit Unit;

        public override bool IsConditionMet(BattleStateMachine battleStateMachine)
        {
            return Unit == null;
        }
    }
}
