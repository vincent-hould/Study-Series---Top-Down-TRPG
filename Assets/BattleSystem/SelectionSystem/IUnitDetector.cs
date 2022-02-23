using UnityEngine;

namespace TopDownTRPG
{
    public interface IUnitDetector
    {
        public GameObject FindUnit(Vector3 position);
    }
}
