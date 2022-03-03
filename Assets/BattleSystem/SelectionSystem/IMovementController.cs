using UnityEngine;

namespace TopDownTRPG
{
    public interface IMovementController
    {
        public Vector3 GetMovement(CursorConstraint cursorConstraint);
    }
}
