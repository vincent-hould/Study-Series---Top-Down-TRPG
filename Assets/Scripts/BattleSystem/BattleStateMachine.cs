using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class BattleStateMachine : MonoBehaviour
    {
        [SerializeField] private List<Faction> Factions;
        private EndCondition[] EndConditions;

        private BaseState _state;
        private int _factionIndex = -1;
        private bool _onGoing = false;

        private void Awake()
        {
            BattleEventChannelSO.OnAttackRequested += Attack;
            BattleEventChannelSO.OnMoveRequested += Move;
            BattleEventChannelSO.OnEndTurnRequested += EndTurn;

            EndConditions = GetComponents<EndCondition>();
        }

        private void OnDestroy()
        {
            BattleEventChannelSO.OnAttackRequested -= Attack;
            BattleEventChannelSO.OnMoveRequested -= Move;
            BattleEventChannelSO.OnEndTurnRequested -= EndTurn;
        }

        private void Start()
        {
            _onGoing = true;
            SetState(new IntroState(this));
        }

        private void Update()
        {
            // TODO Update state machine for tarodev version with tick to move checking condition only
            // in the right states instead of having this variable
            if (_onGoing)
                CheckEndConditions();
        }

        public void SetState(BaseState state)
        {
            if (_state != null)
                StartCoroutine(_state.Leave());

            _state = state;
            if (_state != null)
                StartCoroutine(_state.Enter());
        }

        public BaseState GetNextFactionState()
        {
            int factionCount = Factions.Count;
            int nextIndex = ++_factionIndex < factionCount ? _factionIndex : 0;
            Faction nextFaction = Factions[nextIndex];
            return nextFaction.Controllable 
                ? new ControllableFactionTurnState(this, nextFaction) as BaseState
                : new AIFactionTurnState(this, nextFaction) as BaseState;
        }

        private void CheckEndConditions()
        {
            foreach (EndCondition endCondition in EndConditions)
            {
                if (endCondition.IsConditionMet(this))
                {
                    _onGoing = false;
                    SetState(new OutroState(this, endCondition.IsWin));
                    break;
                }
            }
        }

        private void Attack() => StartCoroutine(_state.Attack());

        private void Move() => StartCoroutine(_state.Move());

        private void EndTurn() => StartCoroutine(_state.EndTurn());
    }
}
