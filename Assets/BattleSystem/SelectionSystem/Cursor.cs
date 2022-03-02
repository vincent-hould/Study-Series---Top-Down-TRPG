using UnityEngine;

namespace TopDownTRPG
{
    public class Cursor : MonoBehaviour
    {
        public delegate void CursorSelection(Selection selection);
        public event CursorSelection OnCursorSelection;

        private ISelectableDetector _selectableDetector;

        private void Awake() {
            _selectableDetector = GetComponent<ISelectableDetector>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                ISelectable selectable = _selectableDetector.FindSelectable(transform.position);
                Selection selection = new Selection(transform.position, selectable);
                if (OnCursorSelection != null)
                {
                    OnCursorSelection(selection);
                }
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
