using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class EnemyTurnState : BaseState
    {
        public EnemyTurnState(BattleStateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Enter()
        {
            _stateMachine.TitleText.Display("Enemy Turn", 2f);
            yield return new WaitForSeconds(4f);
            _stateMachine.SetState(new PlayerTurnState(_stateMachine));
        }

        public override IEnumerator Leave()
        {
            BattleEventChannelSO.RaiseUnitRefreshed();
            yield break;
        }
    }
}
