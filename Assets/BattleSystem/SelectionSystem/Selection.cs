using UnityEngine;

namespace TopDownTRPG
{
    public class Selection
    {
        public Vector3 Position { get; private set; }
        public GameObject Unit { get; private set; }

        public Selection(Vector3 position, GameObject unit)
        {
            Position = position;
            Unit = unit;
        }
    }
}
