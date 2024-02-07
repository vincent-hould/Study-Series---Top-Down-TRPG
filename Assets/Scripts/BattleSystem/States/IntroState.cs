using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class IntroState : BaseState
    {
        public IntroState(BattleStateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Enter()
        {
            var header = UIManager.Instance.GetHeader();
            yield return header.Display("Let's Fight !", 2f);

            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }
    }
}
