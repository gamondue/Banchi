using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banchi; 

namespace ProgettoTestNUnit
{
    internal class TestDataLayer
    {
        [SetUp]
        public void Setup()
        {
            DataLayer.Inizializzazioni();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
