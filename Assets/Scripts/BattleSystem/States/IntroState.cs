using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class IntroState : BaseState
    {
        public IntroState(BattleStateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Enter()
        {
            UIEventChannelSO.RaiseHeaderTextRequest("Let's Fight !", 2f);
            yield return new WaitForSeconds(2f);

            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }
    }
}
