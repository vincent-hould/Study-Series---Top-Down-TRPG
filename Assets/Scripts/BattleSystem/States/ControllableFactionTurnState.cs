using System.Collections;
using UnityEngine;

namespace TopDownTRPG
{
    public class ControllableFactionTurnState : BaseState
    {
        private Unit _selectedUnit;
        private Faction _faction;

        public ControllableFactionTurnState(BattleStateMachine stateMachine, Faction faction) : base(stateMachine)
        {
            _faction = faction;
        }

        public override IEnumerator Enter()
        {
            UIEventChannelSO.RaiseHeaderTextRequest(_faction.Name + " Turn", 2f);
            yield return new WaitForSeconds(2f);
            _selectedUnit = null;
            SelectionEventChannelSO.OnSelectionDone += OnSelectionDone;
            SelectionEventChannelSO.RaiseSelectionRequest(new UnitSelectionCursorConstraint(_faction));
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

            Unit selectedUnit = (Unit)selection.Selectable;
            if (_selectedUnit && selectedUnit && _selectedUnit != selectedUnit)
            {
                _selectedUnit.Attack(selectedUnit);
                _selectedUnit = null;
                SelectionEventChannelSO.RaiseSelectionRequest(new UnitSelectionCursorConstraint(_faction));
            }
            else if (_selectedUnit && !selectedUnit)
            {
                _selectedUnit.OnMovementDone += OnMovementDone;
                _selectedUnit.Move(selection.Position);
            }
            else if (!_selectedUnit && selectedUnit && selectedUnit.Faction == _faction)
            {
                _selectedUnit = selectedUnit;
                _selectedUnit.SetSelected();
                UIEventChannelSO.RaiseContextualMenuDisplayRequest(_selectedUnit.transform.position, _selectedUnit.CanBeSelected(), !_selectedUnit.HasMoved);
            }
            else if (!_selectedUnit && !selectedUnit)
            {
                UIEventChannelSO.RaiseContextualMenuDisplayRequest(selection.Position, false, false);
            }
        }

        private void OnMovementDone(Unit unit)
        {
            unit.OnMovementDone -= OnMovementDone;
            _selectedUnit = null;
            SelectionEventChannelSO.RaiseSelectionRequest(new UnitSelectionCursorConstraint(_faction));
        }

        public override IEnumerator Attack()
        {
            SelectionEventChannelSO.RaiseSelectionRequest(new AttackCursorConstraint(_selectedUnit));
            UIEventChannelSO.RaiseContextualMenuHideRequest();
            yield break;
        }

        public override IEnumerator Move()
        {
            SelectionEventChannelSO.RaiseSelectionRequest(new MoveCursorConstraint(_selectedUnit));
            UIEventChannelSO.RaiseContextualMenuHideRequest();
            yield break;
        }

        public override IEnumerator EndTurn()
        {
            _stateMachine.SetState(_stateMachine.GetNextFactionState());
            UIEventChannelSO.RaiseContextualMenuHideRequest();
            yield break;
        }
    }
}
