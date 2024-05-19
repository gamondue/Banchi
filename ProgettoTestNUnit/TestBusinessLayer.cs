using Banchi;

namespace ProgettoTestNUnit
{
    public class TestBusinessLayer
    {
        [SetUp]
        public void Setup()
        {
            BusinessLayer.Inizializzazioni();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}