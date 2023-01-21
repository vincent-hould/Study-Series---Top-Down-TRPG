using UnityEngine;

namespace TopDownTRPG
{
    public class ExhaustCommand : ICommand
    {
        private Unit _unit;

        public ExhaustCommand(Unit unit)
        {
            _unit = unit;
        }

        public void Execute()
        {
            Debug.Log(_unit.gameObject.name + " was exhausted.");
            _unit.Exhaust();
        }
    }
}
