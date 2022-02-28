using UnityEngine;

namespace TopDownTRPG
{
    public class Cursor : MonoBehaviour
    {
        public delegate void CursorSelection(Selection selection);
        public event CursorSelection OnCursorSelection;

        private IUnitDetector _unitDetector;

        private void Awake() {
            _unitDetector = GetComponent<IUnitDetector>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
            {
                Unit unit = _unitDetector.FindUnit(transform.position);
                Selection selection = new Selection(transform.position, unit);
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
