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
            var header = UIManager.Instance.GetHeader();
            yield return header.Display(text, 20f);
        }
    }
}
