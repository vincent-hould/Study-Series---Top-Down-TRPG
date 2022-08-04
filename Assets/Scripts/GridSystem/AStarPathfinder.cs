using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace TopDownTRPG
{
    public class AStarPathfinder : MonoBehaviour, IPathfinder
    {
        public List<Node> FindPath(Node startNode, Node targetNode)
        {
            var toSearch = new List<Node>() { startNode };
            var processed = new List<Node>();
            while (toSearch.Any())
            {
                var current = toSearch[0];
                foreach (var t in toSearch)
                {
                    if (t.F < current.F || t.F == current.F && t.H < current.H)
                        current = t;
                }

                processed.Add(current);
                toSearch.Remove(current);

                if (current == targetNode)
                {
                    var currentPathTile = targetNode;
                    var path = new List<Node>();
                    var count = 100;
                    while (currentPathTile != startNode)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                        count--;
                        if (count < 0)
                            throw new Exception();
                    }

                    return path;
                }

                foreach (var neighbor in current.Neighbors.Where(t => t.IsWalkable() && !processed.Contains(t)))
                {
                    var inSearch = toSearch.Contains(neighbor);
                    var costToNeighbor = current.G + current.GetDistance(neighbor);
                    if (!inSearch || costToNeighbor < neighbor.G)
                    {
                        neighbor.G = costToNeighbor;
                        neighbor.Connection = current;
                        if (!inSearch)
                        {
                            neighbor.H = neighbor.GetDistance(targetNode);
                            toSearch.Add(neighbor);
                        }
                    }
                }
            }

            return null;
        }
    }
}
