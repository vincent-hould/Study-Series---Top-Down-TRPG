using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class OutroState : BaseState
    {
        private bool _isWin;

        public OutroState(BattleStateMachine stateMachine, bool isWin) : base(stateMachine)
        {
            _isWin = isWin;
        }

        public override IEnumerator Enter()
        {
            var text = _isWin ? "Victory !" : "Defeat...";
            UIEventChannelSO.RaiseHeaderTextRequest(text, 20f);
            yield break;
        }
    }
}
