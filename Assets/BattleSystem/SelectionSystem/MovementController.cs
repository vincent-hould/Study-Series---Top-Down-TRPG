using UnityEngine;

namespace TopDownTRPG
{
    public class MovementController : MonoBehaviour
    {
        private IMover _mover;

        private void Awake()
        {
            _mover = GetComponent<IMover>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _mover.Move(transform.position + Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _mover.Move(transform.position + Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _mover.Move(transform.position + Vector3.down);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                _mover.Move(transform.position + Vector3.right);
            }
        }
    }
}
