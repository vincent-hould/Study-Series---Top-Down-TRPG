using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class GridMover : MonoBehaviour, IMover
    {
        private Unit _unit;
        private bool _isMoving = false;
        private Queue<Vector3> _moveTiles;

        private void Awake()
        {
            _unit = GetComponent<Unit>();
        }

        private void Update()
        {
            if (!_isMoving)
                return;

            if (_moveTiles.Count > 0)
            {
                if (!transform.position.Equals(_moveTiles.Peek()))
                {
                    transform.position = Vector3.MoveTowards(transform.position, _moveTiles.Peek(), Time.deltaTime * 3f);
                }
                else
                {
                    _moveTiles.Dequeue();
                }
            }
            else
            {
                _isMoving = false;
                _unit.OnMoveDone();
            }
        }

        public void Move(Vector3 destination)
        {
            if (_isMoving)
                return;

            _isMoving = true;
            List<Vector3> path = GridManager.Instance.FindPath(_unit.transform.position, destination);
            _moveTiles = new Queue<Vector3>();
            foreach (var tile in path)
                _moveTiles.Enqueue(tile);
        }
    }
}
