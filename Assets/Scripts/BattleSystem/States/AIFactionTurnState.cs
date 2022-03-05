using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class AIFactionTurnState : BaseState
    {
        private Faction _faction;

        public AIFactionTurnState(BattleStateMachine stateMachine, Faction faction) : base(stateMachine)
        {
            _faction = faction;
        }

        public override IEnumerator Enter()
        {
            _stateMachine.TitleText.Display(_faction.Name + " Turn", 2f);
            yield return new WaitForSeconds(4f);
            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }

        public override IEnumerator Leave()
        {
            BattleEventChannelSO.RaiseUnitRefreshed();
            yield break;
        }
    }
}
