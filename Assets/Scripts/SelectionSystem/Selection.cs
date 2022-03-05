using UnityEngine;

namespace TopDownTRPG
{
    public class Selection
    {
        public Vector3 Position { get; private set; }
        public ISelectable Selectable { get; private set; }

        public Selection(Vector3 position, ISelectable selectable)
        {
            Position = position;
            Selectable = selectable;
        }

        public override string ToString()
        {
            return "position:" + Position.ToString() + ", selectable: " + Selectable?.ToString();
        }
    }
}
