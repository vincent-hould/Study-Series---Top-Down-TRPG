using UnityEngine;

namespace TopDownTRPG
{
    public class AttackCommand : ICommand
    {
        private Unit _attacker;
        private Unit _target;

        public AttackCommand(Unit attacker, Unit target)
        {
            _attacker = attacker;
            _target = target;
        }

        public void Execute()
        {
            _attacker.Attack(_target);
        }
    }
}
