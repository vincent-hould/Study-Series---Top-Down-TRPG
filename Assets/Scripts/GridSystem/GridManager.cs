using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace TopDownTRPG
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Tilemap GroundTilemap;
        [SerializeField] private Tilemap NonWalkableTilemap;

        public static GridManager Instance { get; private set; }

        private ISelectableDetector _selectableDetector;
        private IPathfinder _pathfinder;
        private Dictionary<Vector3, Node> grid;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            _selectableDetector = GetComponent<ISelectableDetector>();
            _pathfinder = GetComponent<IPathfinder>();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            grid = new Dictionary<Vector3, Node>();
            foreach (var position in GroundTilemap.cellBounds.allPositionsWithin)
            {
                if (!GroundTilemap.HasTile(position))
                    continue;

                bool isWalkable = NonWalkableTilemap.GetTile(NonWalkableTilemap.WorldToCell(position)) == null;
                grid.Add(position, new Node(position, isWalkable));
            }

            foreach(var node in grid)
                node.Value.CacheNeighbors(grid);
        }

        public ISelectable FindSelectable(Vector3 position)
        {
            return _selectableDetector.FindSelectable(position);
        }

        public bool IsWalkable(Vector3 position)
        {
            Node node;
            bool isValidPosition = grid.TryGetValue(position, out node);
            return isValidPosition && node.Walkable && FindSelectable(position) == null;
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

        public List<Vector3> FindPath(Vector3 origin, Vector3 destination)
        {
            Node originNode, destinationNode;
            bool validOrigin = grid.TryGetValue(GroundTilemap.WorldToCell(origin), out originNode);
            bool validDestination = grid.TryGetValue(GroundTilemap.WorldToCell(destination), out destinationNode);
            if (!validOrigin || !validDestination)
                return new List<Vector3>();

            List<Node> nodes = _pathfinder.FindPath(originNode, destinationNode);
            List<Vector3> pathTiles = nodes.Select(node => (GroundTilemap.CellToWorld(node.Position) + new Vector3(0.5f, 0.5f))).Reverse().ToList();

            return pathTiles;
        }
    }
}
