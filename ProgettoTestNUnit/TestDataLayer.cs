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
        public void TestDatiAule()
        {
            List<Aula> aule = new();
            Aula aula = new("L13", 500.0, 1000.0);
            aule.Add(aula);
            aula = new("P12", 600.0, 800.0);
            aule.Add(aula);
            aula = new("L10", 700.0, 700.0);
            aule.Add(aula);
            DataLayer.ScriviTutteLeAule(aule);
            Assert.That(aule, Is.EqualTo(DataLayer.LeggiTutteLeAule()));
        }
    }
}
