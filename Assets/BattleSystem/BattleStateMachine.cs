using UnityEngine;

namespace TopDownTRPG
{
    public class BattleStateMachine : MonoBehaviour
    {
        [SerializeField]
        public TextManager TitleText;

        private BaseState _state;

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

        private void Start()
        {
            SetState(new IntroState(this));
        }
    }
}
