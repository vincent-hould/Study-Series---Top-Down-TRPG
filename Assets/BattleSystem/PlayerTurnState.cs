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
            if (selection.Selectable != null && selection.Selectable.GetType() != typeof(Unit)) return;

            Unit selectedUnit = (Unit) selection.Selectable;
            if (_selectedUnit && selectedUnit && _selectedUnit != selectedUnit)
            {
                _selectedUnit.Attack(selectedUnit);
                _selectedUnit = null;
                SelectionEventChannelSO.RaiseSelectionRequest();
            }
            else if (_selectedUnit && !selectedUnit)
            {
                _selectedUnit.OnMovementDone += OnMovementDone;
                _selectedUnit.Move(selection.Position);
            }
            else if (!_selectedUnit && selectedUnit)
            {
                _selectedUnit = selectedUnit;
                _selectedUnit.SetSelected();
                SelectionEventChannelSO.RaiseSelectionRequest();
            }
            else if (!_selectedUnit && !selectedUnit)
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
