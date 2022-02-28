using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class PlayerTurnState : BaseState
    {
        private Unit _selectedUnit;

        public PlayerTurnState(BattleStateMachine stateMachine) : base(stateMachine) { }

        public override IEnumerator Enter()
        {
            _stateMachine.TitleText.Display("Player Turn", 2f);
            yield return new WaitForSeconds(2f);
            _selectedUnit = null;
            SelectionEventChannelSO.OnSelectionDone += OnSelectionDone;
            SelectionEventChannelSO.RaiseSelectionRequest();
            yield break;
        }

        public override IEnumerator Leave()
        {
            SelectionEventChannelSO.OnSelectionDone -= OnSelectionDone;
            BattleEventChannelSO.RaiseUnitRefreshed();
            yield break;
        }

        private void OnSelectionDone(Selection selection)
        {
            if (_selectedUnit && selection.Unit && _selectedUnit != selection.Unit)
            {
                _selectedUnit.Attack(selection.Unit);
                _selectedUnit = null;
                SelectionEventChannelSO.RaiseSelectionRequest();
            }
            else if (_selectedUnit && !selection.Unit)
            {
                _selectedUnit.OnMovementDone += OnMovementDone;
                _selectedUnit.Move(selection.Position);
            }
            else if (!_selectedUnit && selection.Unit)
            {
                _selectedUnit = selection.Unit;
                _selectedUnit.SetSelected();
                SelectionEventChannelSO.RaiseSelectionRequest();
            }
            else if (!_selectedUnit && !selection.Unit)
            {
                _stateMachine.SetState(new EnemyTurnState(_stateMachine));
            }
        }

        private void OnMovementDone(Unit unit)
        {
            unit.OnMovementDone -= OnMovementDone;
            _selectedUnit = null;
            SelectionEventChannelSO.RaiseSelectionRequest();
        }
    }
}
