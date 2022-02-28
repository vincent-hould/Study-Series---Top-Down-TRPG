using UnityEngine;

namespace TopDownTRPG
{
    public interface IUnitDetector
    {
        public Unit FindUnit(Vector3 position);
    }
}
