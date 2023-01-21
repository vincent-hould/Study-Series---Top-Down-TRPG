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
            yield return new WaitForSeconds(4f);

            var units = GetUnits();
            foreach (Unit unit in units)
            {
                while(unit.CanBeSelected())
                {
                    var action = ChooseAction(unit);
                    action.Execute();
                }
            }
            
            _stateMachine.SetState(_stateMachine.GetNextFactionState());
        }

        public override IEnumerator Leave()
        {
            BattleEventChannelSO.RaiseUnitRefreshed();
            yield break;
        }

        private List<Unit> GetUnits()
        {
            return Unit.SpawnedUnits.Where(unit => unit.Faction == _faction).ToList();
        }

        private ICommand ChooseAction(Unit unit)
        {
            // Move to a script call on unit, IBehavior
            return new ExhaustCommand(unit);
        }
    }
}
