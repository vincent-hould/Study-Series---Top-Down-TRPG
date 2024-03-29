using System;
using UnityEngine;

namespace TopDownTRPG
{
    public class Cursor : MonoBehaviour
    {
        private IMovementController _movementController;
        private IMover _mover;
        private CursorConstraint _cursorConstraint;
        private Action<Selection> _selectionCallback;

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

            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
                Select(false);
        }

        public void Show(CursorConstraint cursorConstraint, Action<Selection> selectionCallback)
        {
            _selectionCallback = selectionCallback;
            _cursorConstraint = cursorConstraint;
            gameObject.SetActive(true);
            SelectionEventChannelSO.RaiseCursorEnabled(transform);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            SelectionEventChannelSO.RaiseCursorDisabled();
        }

        private void Select(bool isSelection = true)
        {
            Selection selection;
            if (isSelection)
            {
                ISelectable selectable = GridManager.Instance.FindSelectable(transform.position);
                selection = new Selection(transform.position, selectable);
                if (!_cursorConstraint.CanSelect(selection))
                {
                    selection = new Selection(selection.Position, null);
                    if (!_cursorConstraint.CanSelect(selection))
                        return;
                }
            }
            else if (_cursorConstraint.CanBeCancelled())
            {
                selection = null;
            }
            else
            {
                return;
            }

            Hide();
            _selectionCallback(selection);
        }
    }
}
