using System.Collections.Generic;

namespace TopDownTRPG
{
    public interface IPathfinder
    {
        public List<Node> FindPath(Node startNode, Node targetNode);
    }
}
