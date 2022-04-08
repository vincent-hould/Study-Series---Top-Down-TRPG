using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class BattleStateMachine : MonoBehaviour
    {
        [SerializeField]
        private List<Faction> Factions;

        private BaseState _state;
        private int _factionIndex = -1;

        private void Awake()
        {
            BattleEventChannelSO.OnAttackRequested += Attack;
            BattleEventChannelSO.OnMoveRequested += Move;
            BattleEventChannelSO.OnEndTurnRequested += EndTurn;
        }

        private void OnDestroy()
        {
            BattleEventChannelSO.OnAttackRequested -= Attack;
            BattleEventChannelSO.OnMoveRequested -= Move;
            BattleEventChannelSO.OnEndTurnRequested -= EndTurn;
        }

        private void Start()
        {
            SetState(new IntroState(this));
        }

        public void SetState(BaseState state)
        {
            if (_state != null)
            {
                StartCoroutine(_state.Leave());
            }

            _state = state;

            if (_state != null)
            {
                StartCoroutine(_state.Enter());
            }
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

        private void Attack()
        {
            StartCoroutine(_state.Attack());
        }

        private void Move()
        {
            StartCoroutine(_state.Move());
        }

        private void EndTurn()
        {
            StartCoroutine(_state.EndTurn());
        }
    }
}
