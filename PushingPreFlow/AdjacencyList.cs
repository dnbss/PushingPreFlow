using System;
using Map;

namespace PushingPreFlow
{
    public class AdjacencyList
    {
        private Map<string, Map<string, DataEdge>> adjacencyList;

        public Map.Queue<string> Vertexes => adjacencyList.GetKeys();
        
        public AdjacencyList()
        {
            adjacencyList = new Map<string, Map<string, DataEdge>>();
        }

        public Map<string, DataEdge> this[int index] => adjacencyList[index];

        public Map<string, DataEdge> this[string vertex] => adjacencyList[vertex];

        public int Length => adjacencyList.GetKeys().Count;
        
        public void Insert(string newNode, string nextNode, DataEdge edge)
        {
            try
            {
                 adjacencyList.FindNode(newNode).data.Insert(nextNode, edge);
            }
            catch (Exception e)
            {
                var t = new Map<string, DataEdge>();
                
                t.Insert(nextNode, edge);
                adjacencyList.Insert(newNode, t);
            }

        }
    }
}