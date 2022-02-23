using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class IntroState : BaseState
    {
        public IntroState(BattleStateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Enter()
        {
            Debug.Log("Battle Start !");
            yield return new WaitForSeconds(2f);

            _stateMachine.SetState(new PlayerTurnState(_stateMachine));
        }
    }
}
