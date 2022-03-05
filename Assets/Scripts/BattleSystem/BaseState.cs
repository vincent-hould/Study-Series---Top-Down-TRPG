using System.Collections;

namespace TopDownTRPG
{
    public abstract class BaseState
    {
        protected BattleStateMachine _stateMachine;

        public BaseState(BattleStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual IEnumerator Enter()
        {
            yield break;
        }

        public virtual IEnumerator Leave()
        {
            yield break;
        }
    }
}
