using UnityEngine;

namespace TopDownTRPG
{
    public class Cursor : MonoBehaviour
    {
        public delegate void CursorSelection(Selection selection);
        public event CursorSelection OnCursorSelection;

        private IMovementController _movementController;
        private IMover _mover;

        private CursorConstraint _cursorConstraint;

        private void Awake() {
            _movementController = GetComponent<IMovementController>();
            _mover = GetComponent<IMover>();
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 movement = _movementController.GetMovement(_cursorConstraint);
            _mover.Move(movement);

            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                ISelectable selectable = GridManager.Instance.FindSelectable(transform.position);
                Selection selection = new Selection(transform.position, selectable);
                if (_cursorConstraint.CanSelect(selection) && OnCursorSelection != null)
                {
                    OnCursorSelection(selection);
                }
            }
        }

        public void Enable(CursorConstraint cursorConstraint)
        {
            _cursorConstraint = cursorConstraint;
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            _cursorConstraint = null;
            gameObject.SetActive(false);
        }
    }
}
