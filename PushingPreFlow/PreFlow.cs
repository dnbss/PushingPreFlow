using System;
using System.Runtime.CompilerServices;
using Floyd;
using Map;

namespace PushingPreFlow
{
    public static class PreFlow
    {
        private static Map<string, DataVertex> vertexesExcessHeight;    

        private static AdjacencyList flowCapacityList;

        public static int FindMaxFlow(AdjacencyList adList)
        {
            flowCapacityList = adList;
            
            InitializePreFlow();

            var t = FindExcessVertex();
            
            while (ExistExcess())
            {
                if (IsPushing(t))
                {
                    Push(t);
                }
                else if (IsRelabeling(t))
                {
                    Relabel(t);
                }
                else
                {
                    t = FindExcessVertex();
                }
                
            }

            int maxFlow = 0;
            for (int j = 0; j < flowCapacityList["S"].Count; j++)
            {
                maxFlow += flowCapacityList["S"][j].Flow;
            }

            return maxFlow;
        }

        public static void Print()
        {
            for (int i = 0; i < flowCapacityList.Length; i++)
            {
                for (int j = 0; j < flowCapacityList[i].Count; j++)
                {
                    Console.Write($"{flowCapacityList.Vertexes[i]}-[{flowCapacityList[i][j].Flow}" +
                                  $"|{flowCapacityList[i][j].Capacity}]-> {flowCapacityList[i].GetKeys()[j]}   ");
                }
                
                Console.WriteLine();
            }
        }

        private static string FindExcessVertex()
        {
            for (int i = 0; i < vertexesExcessHeight.Count; i++)
            {
                if (vertexesExcessHeight[i].Excess > 0 && vertexesExcessHeight.GetKeys()[i] != "S" && vertexesExcessHeight.GetKeys()[i] != "T")
                {
                    return vertexesExcessHeight.GetKeys()[i];
                }
            }

            throw new Exception("there are no excess vertices");
        }

        private static bool ExistExcess()
        {
            for (int i = 0; i < vertexesExcessHeight.Count; i++)
            {
                if (vertexesExcessHeight[i].Excess > 0 && vertexesExcessHeight.GetKeys()[i] != "S" && vertexesExcessHeight.GetKeys()[i] != "T")
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsRelabeling(string v) 
        {
            if (vertexesExcessHeight[v].Excess == 0)
            {
                return false;
            }

            for (int i = 0; i < vertexesExcessHeight.Count; i++)
            {
                if (ResidualThroughput(v,vertexesExcessHeight.GetKeys()[i]) > 0)
                {
                    if (vertexesExcessHeight[v].Height > vertexesExcessHeight[i].Height)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsPushing(string v)
        {
            try
            {
                var u = PushableVertex(v);
                
                return vertexesExcessHeight[v].Excess > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static string PushableVertex(string v)
        {
            for (int i = 0; i < vertexesExcessHeight.Count; i++)
            {
                if (vertexesExcessHeight[v].Height == vertexesExcessHeight[i].Height + 1 
                    && ResidualThroughput(v, vertexesExcessHeight.GetKeys()[i]) > 0)
                {
                    return vertexesExcessHeight.GetKeys()[i];
                }
            }
            
            throw new Exception("the vertex does not exist");
        }

        private static void InitializePreFlow()
        {
            vertexesExcessHeight = new Map<string, DataVertex>();

            for (int i = 0; i < flowCapacityList.Length; i++)
            {
                vertexesExcessHeight.Insert(flowCapacityList.Vertexes[i], new DataVertex() { Height = 0, Excess = 0});
            }
            
            vertexesExcessHeight.Insert("T", new DataVertex() {Height = 0, Excess = 0});

            for (int i = 0; i < flowCapacityList["S"].Count; i++)
            {
                var t = flowCapacityList["S"].GetKeys()[i];
                
                flowCapacityList["S"][t].Flow = flowCapacityList["S"][t].Capacity;

                /*flowCapacityList.Insert(t, "S"
                    , new DataEdge(){Capacity = flowCapacityList["S"][t].Capacity, Flow = -flowCapacityList["S"][t].Flow});*/
                
                //flowCapacityList[t]["S"].Flow = -flowCapacityList["S"][t].Flow;
                
                vertexesExcessHeight[t].Excess = flowCapacityList["S"][t].Flow; 

                vertexesExcessHeight["S"].Excess -= vertexesExcessHeight[t].Excess;
            }

            vertexesExcessHeight["S"].Height = vertexesExcessHeight.Count;

            
        }

        private static void Push(string from)
        {
            string to = PushableVertex(from);
            
            var d = vertexesExcessHeight[from].Excess < ResidualThroughput(from, to)
                ? vertexesExcessHeight[from].Excess
                : ResidualThroughput(from, to);

            try
            {
                flowCapacityList[from][to].Flow += d;
            }
            catch (Exception e)
            {
                flowCapacityList[to][from].Flow -= d;
            }

            vertexesExcessHeight[from].Excess -= d;

            vertexesExcessHeight[to].Excess += d;
        }

        private static void Relabel(string v)
        {
            int minHeight = Int32.MaxValue;

            for (int i = 0; i < vertexesExcessHeight.Count; i++)
            {
                if (ResidualThroughput(v, vertexesExcessHeight.GetKeys()[i]) > 0 
                    && vertexesExcessHeight[i].Height < minHeight)
                {
                    minHeight = vertexesExcessHeight[i].Height;
                }
            }

            vertexesExcessHeight[v].Height = minHeight + 1;
        }

        private static int ResidualThroughput(string u, string v)
        {
            try
            {
                return flowCapacityList[u][v].Capacity - flowCapacityList[u][v].Flow;
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                return flowCapacityList[v][u].Flow;
            }
            catch (Exception e)
            {
                // ignored
            }

            return 0;
        }
    }
}