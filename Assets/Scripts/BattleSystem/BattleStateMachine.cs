using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class BattleStateMachine : MonoBehaviour
    {
        [SerializeField]
        public TextManager TitleText;
        [SerializeField]
        public GameObject ContextualMenuPanel;
        [SerializeField]
        private List<Faction> Factions;

        private BaseState _state;
        private int _factionIndex = -1;

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

        public void DisplayMenu(Vector3 position)
        {
            var menuPosition = position + Vector3.right * 2;
            ContextualMenuPanel.SetActive(true);
            ContextualMenuPanel.transform.position = Camera.main.WorldToScreenPoint(menuPosition);
        }

        public void HideMenu()
        {
            ContextualMenuPanel.SetActive(false);
        }
        
        public void Attack()
        {
            StartCoroutine(_state.Attack());
        }

        public void Move()
        {
            StartCoroutine(_state.Move());
        }

        public void EndTurn()
        {
            StartCoroutine(_state.EndTurn());
        }
    }
}
