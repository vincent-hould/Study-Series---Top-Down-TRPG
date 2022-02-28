using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class IntroState : BaseState
    {
        public IntroState(BattleStateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Enter()
        {
            _stateMachine.TitleText.Display("Let's Fight !", 2f);
            yield return new WaitForSeconds(2f);

            _stateMachine.SetState(new PlayerTurnState(_stateMachine));
        }
    }
}
