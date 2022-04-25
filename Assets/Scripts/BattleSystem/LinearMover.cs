using UnityEngine;

namespace TopDownTRPG
{
    public class LinearMover : MonoBehaviour, IMover
    {
        private Unit _unit;
        private bool _isMoving = false;
        private Vector3 _destination;

        private void Awake()
        {
            _unit = GetComponent<Unit>();    
        }

        private void Update()
        {
            if (_isMoving && !transform.position.Equals(_destination))
            {
                transform.position = Vector3.MoveTowards(transform.position, _destination, Time.deltaTime * 3f);
            }
            else if (_isMoving)
            {
                _isMoving = false;
                _unit.OnMoveDone();
            }
        }

        public void Move(Vector3 destination)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                _destination = destination;
            }
        }
    }
}
