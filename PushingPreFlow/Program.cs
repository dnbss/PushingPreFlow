using System;
using Floyd;

namespace PushingPreFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            AdjacencyList adList = FileHandler.CreateAdjacencyList(@"..\..\..\network.txt");
            
            Console.WriteLine($"Max Flow = {PreFlow.FindMaxFlow(adList)}");
            
            PreFlow.Print();
        }
    }
}