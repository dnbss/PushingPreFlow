using System;
using System.IO;
using PushingPreFlow;

namespace Floyd
{
    public static class FileHandler
    {
        public static AdjacencyList CreateAdjacencyList(string path)
        {
            string[] allStrings = File.ReadAllLines(path);

            AdjacencyList adjacencyList = new AdjacencyList();

            for (int i = 0; i < allStrings.Length; i++)
            {
                string from = allStrings[i].Substring(0, allStrings[i].IndexOf(' '));

                allStrings[i] = allStrings[i].Substring(allStrings[i].IndexOf(' ') + 1);
                
                string to = allStrings[i].Substring(0, allStrings[i].IndexOf(' '));
                
                allStrings[i] = allStrings[i].Substring(allStrings[i].IndexOf(' ') + 1);

                int capacity = int.Parse(allStrings[i]);

                adjacencyList.Insert(from, to, new DataEdge() { Capacity = capacity, Flow = 0});
            }

            return adjacencyList;
        }
    }
}