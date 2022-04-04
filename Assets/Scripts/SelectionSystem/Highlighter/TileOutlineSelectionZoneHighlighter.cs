using UnityEngine;
using System.Collections.Generic;

namespace TopDownTRPG
{
    public class TileOutlineSelectionZoneHighlighter : MonoBehaviour, ISelectionZoneHighlighter
    {
        [SerializeField]
        private GameObject attackHighlightPrefab;
        [SerializeField]
        private GameObject moveHighlightPrefab;

        private List<GameObject> _highlights;

        public void Highlight(CursorConstraint cursorConstraint)
        {
            List<Vector3> tiles = cursorConstraint.GetAllowedTiles();
            GameObject highlightPrefab = GetHighlightPrefab(cursorConstraint);
            _highlights = new List<GameObject>();
            foreach(var tile in tiles)
            {
                var highlight = Instantiate(highlightPrefab, tile, Quaternion.identity);
                highlight.transform.parent = transform;
                _highlights.Add(highlight);
            }
        }

        public void RemoveHighlight()
        {
            // TODO object pooling
            for (var i = 0; i < _highlights.Count; i++)
            {
                Destroy(_highlights[i]);
            }
            _highlights.Clear();
        }

        private GameObject GetHighlightPrefab(CursorConstraint cursorConstraint)
        {
            return cursorConstraint.GetType() == typeof(AttackCursorConstraint) ? attackHighlightPrefab : moveHighlightPrefab;
        }
    }
}
