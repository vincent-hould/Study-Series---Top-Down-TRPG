using System.Collections;
using UnityEngine;
using TopDownTRRPG.BattleSystem.SelectionSystem;

namespace TopDownTRRPG.BattleSystem
{
    public class PlayerTurnState : BaseState
    {
        public PlayerTurnState(BattleStateMachine stateMachine) : base(stateMachine) { }
        
        public override IEnumerator Enter()
        {
            Debug.Log("Request selection.");
            SelectionEventChannelSO.OnSelectionDone += OnSelectionDone;
            SelectionEventChannelSO.RaiseSelectionRequest();
            yield break;
        }

        private void OnSelectionDone(Selection selection)
        {
            Debug.Log("Receive Selection.");
        }
    }
}
