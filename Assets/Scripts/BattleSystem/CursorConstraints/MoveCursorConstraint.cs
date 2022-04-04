using UnityEngine;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public class MoveCursorConstraint : CursorConstraint
    {
        private Unit _movedUnit;

        public MoveCursorConstraint(Unit unit) : base()
        {
            _movedUnit = unit;
        }

        public override bool CanSelect(Selection selection)
        {
            return selection.Selectable == null &&
                   Vector3.Distance(selection.Position, _movedUnit.transform.position) <= _movedUnit.MovementRange;
        }

        public override List<Vector3> GetAllowedTiles()
        {
            // TODO deal with map edges and non-walkable tiles/obstacles
            var tiles = new List<Vector3>();
            Vector3 moverPosition = _movedUnit.transform.position;
            float minX = moverPosition.x - _movedUnit.MovementRange;
            float maxX = moverPosition.x + _movedUnit.MovementRange;
            float minY = moverPosition.y - _movedUnit.MovementRange;
            float maxY = moverPosition.y + _movedUnit.MovementRange;

            Vector3 tile;
            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    tile = new Vector3(x, y);
                    if (Vector3.Distance(tile, moverPosition) <= _movedUnit.MovementRange)
                        tiles.Add(tile);
                }
            }
            return tiles;
        }
    }
}
