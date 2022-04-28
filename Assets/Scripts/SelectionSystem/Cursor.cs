using UnityEngine;

namespace TopDownTRPG
{
    public class Cursor : MonoBehaviour
    {
        private IMovementController _movementController;
        private IMover _mover;
        private SelectionManager _selectionManager;
        private CursorConstraint _cursorConstraint;

        private void Awake() {
            _movementController = GetComponent<IMovementController>();
            _mover = GetComponent<IMover>();
        }

        private void Update()
        {
            Vector3 movement = _movementController.GetMovement(_cursorConstraint);
            _mover.Move(movement);
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
                Select();
        }

        public void Init(SelectionManager selectionManager, CursorConstraint cursorConstraint)
        {
            _selectionManager = selectionManager;
            _cursorConstraint = cursorConstraint;
            gameObject.SetActive(true);
            SelectionEventChannelSO.RaiseCursorEnabled(transform);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            SelectionEventChannelSO.RaiseCursorDisabled();
        }

        private void Select()
        {
            ISelectable selectable = GridManager.Instance.FindSelectable(transform.position);
            Disable();
            Selection selection = new Selection(transform.position, selectable);
            if (selectable != null && !_cursorConstraint.CanSelect(selection))
                selection = new Selection(transform.position, null);
            if (_cursorConstraint.CanSelect(selection))
                _selectionManager.Select(selection);
        }
    }
}
