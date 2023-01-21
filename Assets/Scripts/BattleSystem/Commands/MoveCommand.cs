using UnityEngine;

namespace TopDownTRPG
{
    public class MoveCommand : ICommand
    {
        private Unit _unitToMove;
        private Vector3 _destination;

        public MoveCommand(Unit unitToMove, Vector3 destination)
        {
            _unitToMove = unitToMove;
            _destination = destination;
        }

        public void Execute()
        {
            _unitToMove.Move(_destination);
        }
    }
}
