using System.Collections;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public class ControllableFactionTurnState : BaseState
    {
        private Unit _selectedUnit;
        private Faction _faction;
        private bool _actionSelected = false;

        public ControllableFactionTurnState(BattleStateMachine stateMachine, Faction faction) : base(stateMachine)
        {
            _faction = faction;
        }

        public override IEnumerator Enter()
        {
            _selectedUnit = null;
            SelectionEventChannelSO.OnSelectionDone += OnSelectionDone;
            SelectionEventChannelSO.OnSelectionCancelled += OnSelectionCancelled;
            var header = UIManager.Instance.GetHeader();
            yield return header.Display(_faction.Name + " Turn", 2f);

            SelectionEventChannelSO.RaiseSelectionRequest(new UnitSelectionCursorConstraint(_faction));
            yield break;
        }

        public override IEnumerator Leave()
        {
            SelectionEventChannelSO.OnSelectionDone -= OnSelectionDone;
            SelectionEventChannelSO.OnSelectionCancelled -= OnSelectionCancelled;
            BattleEventChannelSO.RaiseUnitRefreshed();
            yield break;
        }

        private void OnSelectionDone(Selection selection)
        {
            if (selection.Selectable != null && selection.Selectable.GetType() != typeof(Unit))
                return;

            Unit selectedUnit = (Unit)selection.Selectable;
            if (_selectedUnit && selectedUnit && _selectedUnit != selectedUnit)
            {
                _selectedUnit.Attack(selectedUnit);
                _selectedUnit = null;
                _actionSelected = false;
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
                var actionMenu = UIManager.Instance.GetActionMenu();
                actionMenu.PromptForAction(_selectedUnit.transform.position, BuildActionMenuItems());
            }
            else if (!_selectedUnit && !selectedUnit)
            {
                var actionMenu = UIManager.Instance.GetActionMenu();
                actionMenu.PromptForAction(selection.Position, BuildActionMenuItems());
            }
        }

        private List<ActionMenuItem> BuildActionMenuItems()
        {
            var actionList = new List<ActionMenuItem>
            { 
                new ActionMenuItem("Attack",  () => { Attack(); }, _selectedUnit && _selectedUnit.CanBeSelected()),
                new ActionMenuItem("Move",  () => { Move(); }, _selectedUnit && _selectedUnit.CanBeSelected() && !_selectedUnit.HasMoved),
                new ActionMenuItem("End Turn",  () => { EndTurn(); })
            };
           
            return actionList;
        }

        private void OnSelectionCancelled()
        {
            if (_actionSelected)
            {
                _actionSelected = false;
                var actionMenu = UIManager.Instance.GetActionMenu();
                actionMenu.PromptForAction(_selectedUnit.transform.position, BuildActionMenuItems());
            }
            else
            {
                _selectedUnit.SetSelected(false);
                _selectedUnit = null;
                SelectionEventChannelSO.RaiseSelectionRequest(new UnitSelectionCursorConstraint(_faction));
            }
        }

        private void OnMovementDone(Unit unit)
        {
            _actionSelected = false;
            unit.OnMovementDone -= OnMovementDone;
            _selectedUnit = null;
            SelectionEventChannelSO.RaiseSelectionRequest(new UnitSelectionCursorConstraint(_faction));
        }

        private void Attack()
        {
            _actionSelected = true;
            SelectionEventChannelSO.RaiseSelectionRequest(new AttackCursorConstraint(_selectedUnit));
        }

        private void Move()
        {
            _actionSelected = true;
            SelectionEventChannelSO.RaiseSelectionRequest(new MoveCursorConstraint(_selectedUnit));
        }

        private void EndTurn()
        {
            _actionSelected = false;
            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }
    }
}
