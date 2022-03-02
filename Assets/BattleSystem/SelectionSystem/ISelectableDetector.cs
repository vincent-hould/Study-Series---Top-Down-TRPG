using UnityEngine;

namespace TopDownTRPG
{
    public interface ISelectableDetector
    {
        public ISelectable FindSelectable(Vector3 position);
    }
}
