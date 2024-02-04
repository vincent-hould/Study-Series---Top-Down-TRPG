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

        public virtual bool AreAllowedTilesHighlighted()
        {
            return false;
        }

        public abstract Vector3 GetNextTile(Vector3 position, Vector3 direction);

        public abstract List<Vector3> GetAllowedTiles();
    }
}
