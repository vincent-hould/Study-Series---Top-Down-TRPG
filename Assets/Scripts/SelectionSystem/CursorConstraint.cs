using UnityEngine;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public abstract class CursorConstraint
    {
        public virtual bool CanSelect(Selection selection)
        {
            return selection.Selectable == null || selection.Selectable.CanBeSelected();
        }

        public virtual Vector3 GetNextTile(Vector3 position, Vector3 direction)
        {
            return position + direction;
        }

        public abstract List<Vector3> GetAllowedTiles();
    }
}
