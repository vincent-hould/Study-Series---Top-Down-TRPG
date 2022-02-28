using UnityEngine;

namespace TopDownTRPG
{
    public class Selection
    {
        public Vector3 Position { get; private set; }
        public Unit Unit { get; private set; }

        public Selection(Vector3 position, Unit unit)
        {
            Position = position;
            Unit = unit;
        }

        public override string ToString()
        {
            return "position:" + Position.ToString() + ", unit: " + Unit?.gameObject.name;
        }
    }
}
