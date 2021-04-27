using Floyd;
using NUnit.Framework;
using PushingPreFlow;

namespace PushingPreFlowTest
{
    public class PushingPreFlowTests
    {
        
        [Test]
        public void FindMaxFlowTest_network1_Correct()
        {
            int actual = PreFlow.FindMaxFlow(FileHandler.CreateAdjacencyList(@"..\..\..\network1.txt"));
            
            Assert.AreEqual(4, actual);
        }
        
        [Test]
        public void FindMaxFlowTest_network2_Correct()
        {
            int actual = PreFlow.FindMaxFlow(FileHandler.CreateAdjacencyList(@"..\..\..\network2.txt"));
            
            Assert.AreEqual(8, actual);
        }
        
        [Test]
        public void FindMaxFlowTest_network3_Correct()
        {
            int actual = PreFlow.FindMaxFlow(FileHandler.CreateAdjacencyList(@"..\..\..\network3.txt"));
            
            Assert.AreEqual(25, actual);
        }
        
        [Test]
        public void FindMaxFlowTest_network4_Correct()
        {
            int actual = PreFlow.FindMaxFlow(FileHandler.CreateAdjacencyList(@"..\..\..\network4.txt"));
            
            Assert.AreEqual(16, actual);
        }
    }
}