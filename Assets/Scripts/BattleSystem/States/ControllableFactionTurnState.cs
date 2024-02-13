using System.Collections;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public class ControllableFactionTurnState : BaseState
    {
        private readonly Faction _faction;
        private Unit _selectedUnit;
        private bool _actionSelected = false;

        public ControllableFactionTurnState(BattleStateMachine stateMachine, Faction faction) : base(stateMachine)
        {
            _faction = faction;
        }

        public override IEnumerator Enter()
        {
            SelectionEventChannelSO.OnSelectionDone += OnSelectionDone;
            SelectionEventChannelSO.OnSelectionCancelled += OnSelectionCancelled;
            UIEventChannelSO.OnActionMenuCancelled += OnSelectionCancelled;
            var header = UIManager.Instance.GetHeader();
            yield return header.Display(_faction.Name + " Turn", 2f);

            SelectionManager.Instance.PromptForSelection(new UnitSelectionCursorConstraint(_faction));
        }

        public override IEnumerator Leave()
        {
            _selectedUnit = null;
            _actionSelected = false;
            SelectionEventChannelSO.OnSelectionDone -= OnSelectionDone;
            SelectionEventChannelSO.OnSelectionCancelled -= OnSelectionCancelled;
            UIEventChannelSO.OnActionMenuCancelled -= OnSelectionCancelled;
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
                SelectionManager.Instance.PromptForSelection(new UnitSelectionCursorConstraint(_faction));
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
                new ActionMenuItem("Attack",  () => { SelectAttackAction(); }, _selectedUnit && _selectedUnit.CanBeSelected()),
                new ActionMenuItem("Move",  () => { SelectMoveAction(); }, _selectedUnit && _selectedUnit.CanBeSelected() && !_selectedUnit.HasMoved),
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
                if (_selectedUnit)
                {
                    _selectedUnit.SetSelected(false);
                    _selectedUnit = null;
                }

                SelectionManager.Instance.PromptForSelection(new UnitSelectionCursorConstraint(_faction));
            }
        }

        private void OnMovementDone(Unit unit)
        {
            _actionSelected = false;
            unit.OnMovementDone -= OnMovementDone;
            _selectedUnit = null;
            SelectionManager.Instance.PromptForSelection(new UnitSelectionCursorConstraint(_faction));
        }

        private void SelectAttackAction()
        {
            _actionSelected = true;
            SelectionManager.Instance.PromptForSelection(new AttackCursorConstraint(_selectedUnit));
        }

        private void SelectMoveAction()
        {
            _actionSelected = true;
            SelectionManager.Instance.PromptForSelection(new MoveCursorConstraint(_selectedUnit));
        }

        private void EndTurn()
        {
            _actionSelected = false;
            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }
    }
}
