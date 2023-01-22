using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace TopDownTRPG
{
    public class AIFactionTurnState : BaseState
    {
        private Faction _faction;

        public AIFactionTurnState(BattleStateMachine stateMachine, Faction faction) : base(stateMachine)
        {
            _faction = faction;
        }

        public override IEnumerator Enter()
        {
            UIEventChannelSO.RaiseHeaderTextRequest(_faction.Name + " Turn", 2f);
            yield return new WaitForSeconds(2f);

            var units = GetFactionUnits();
            foreach (Unit unit in units)
            {
                while(unit.CanBeSelected())
                {
                    var action = ChooseAction(unit);
                    action.Execute();
                    yield return new WaitForSeconds(2f);
                }
            }
            
            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }

        public override IEnumerator Leave()
        {
            BattleEventChannelSO.RaiseUnitRefreshed();
            yield break;
        }

        private List<Unit> GetFactionUnits()
        {
            return Unit.SpawnedUnits.Where(unit => unit.Faction == _faction).ToList();
        }

        private ICommand ChooseAction(Unit unit)
        {
            Vector3 position = unit.transform.position;
            // Can we attack someone ?
            var attackableTiles = GridManager.Instance.GetTilesInRange(position, 1);
            Unit target = null;
            Unit possibleTarget;
            foreach (Vector3 attackTarget in attackableTiles)
            {
                possibleTarget = GridManager.Instance.FindSelectable(attackTarget) as Unit;
                if (possibleTarget && unit.Faction.Enemies.Contains(possibleTarget.Faction)) {
                    target = possibleTarget;
                    break;
                }
                
            }

            if (target != null)
            {
                return new AttackCommand(unit, target);
            }

            // Can we move to someone ?
            Vector3? targetTile = null;
            List<Vector3> adjacentTiles;
            var walkableTiles = GridManager.Instance.GetTilesInRange(position, unit.MovementRange, true);
            foreach (Vector3 moveTarget in walkableTiles)
            {
                adjacentTiles = GridManager.Instance.GetTilesInRange(moveTarget, 1);
                foreach(Vector3 adjacentTile in adjacentTiles)
                {
                    possibleTarget = GridManager.Instance.FindSelectable(adjacentTile) as Unit;
                    if (possibleTarget && unit.Faction.Enemies.Contains(possibleTarget.Faction))
                    {
                        targetTile = moveTarget;
                        break;
                    }
                }
            }

            if (targetTile != null) {
                return new MoveCommand(unit, (Vector3)targetTile);
            }

            // Wait for someone to get in our range
            return new ExhaustCommand(unit);
        }
    }
}
