using UnityEngine;

namespace TopDownTRPG
{
    public class MovementController : MonoBehaviour, IMovementController
    {
        public Vector3 GetMovement(CursorConstraint cursorConstraint)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                return cursorConstraint.GetNextTile(transform.position, Vector3.up);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                return cursorConstraint.GetNextTile(transform.position, Vector3.left);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                return cursorConstraint.GetNextTile(transform.position, Vector3.down);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                return cursorConstraint.GetNextTile(transform.position, Vector3.right);

            return transform.position;
        }
    }
}
