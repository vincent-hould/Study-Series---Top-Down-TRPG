using System.Collections.Generic;
using UnityEngine;

namespace TopDownTRPG
{
    public class Node
    {
        private static readonly List<Vector3> Dirs = new List<Vector3> {
            new Vector3(0, 1), new Vector3(0, -1),
            new Vector3(1, 0), new Vector3(-1, 0)
        };

        public Vector3 Position { get; private set; }
        public List<Node> Neighbors { get; private set; }
        public Unit Unit { get; set; }
        public Node Connection { get; set; }
        public float G { get; set; }
        public float H { get; set; }
        public float F => G + H;

        private  bool _walkable = false;

        public Node(Vector3 position, bool walkable)
        {
            Position = position;
            _walkable = walkable;
        }

        public bool IsWalkable() => (_walkable && Unit == null);

        public float GetDistance(Node other) => Vector3.Distance(Position, other.Position);

        public void CacheNeighbors(Dictionary<Vector3, Node> grid)
        {
            Neighbors = new List<Node>();
            Node node;
            foreach (var dir in Dirs)
            {
                bool isValid = grid.TryGetValue(Position + dir, out node);
                if (!isValid)
                    continue;

                Neighbors.Add(node);
            }
        }
    }
}
