using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TopDownTRPG
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField]
        private Tilemap GroundTilemap;
        [SerializeField]
        private Tilemap NonWalkableTilemap;

        private ISelectableDetector _selectableDetector;

        public static GridManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            _selectableDetector = GetComponent<ISelectableDetector>();
        }

        public ISelectable FindSelectable(Vector3 position)
        {
            return _selectableDetector.FindSelectable(position);
        }

        public bool IsWalkable(Vector3 position)
        {
            return FindSelectable(position) == null &&
                   NonWalkableTilemap.GetTile(NonWalkableTilemap.WorldToCell(position)) == null &&
                   GroundTilemap.GetTile(GroundTilemap.WorldToCell(position)) != null;
        }

        public List<Vector3> GetTilesInRange(Vector3 origin, int range, bool walkableOnly = false)
        {
            List<Vector3> tiles = new List<Vector3>();

            float minX = origin.x - range;
            float maxX = origin.x + range;
            float minY = origin.y - range;
            float maxY = origin.y + range;

            Vector3 tile;
            for (var x = minX; x <= maxX; x++)
            {
                for (var y = minY; y <= maxY; y++)
                {
                    tile = new Vector3(x, y);
                    if (Vector3.Distance(tile, origin) <= range && (!walkableOnly || IsWalkable(tile)))
                        tiles.Add(tile);
                }
            }

            return tiles;
        }
    }
}
