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
            // la seguente asserzione non funziona: i due sono oggetti diversi, per cui non sono mai uguali
            //Assert.That(aule, Is.EqualTo(DataLayer.LeggiTutteLeAule()));

            // per verificare che tutti gli elementi delle due liste siano uguali, 
            // bisogna percorrerli con un foreach()
            int i = 0; 
            foreach (Aula a in DataLayer.LeggiTutteLeAule())
            {
                Assert.That(a.BaseInCentimetri, Is.EqualTo(aule[i].BaseInCentimetri));
                Assert.That(a.AltezzaInCentimetri, Is.EqualTo(aule[i].AltezzaInCentimetri));
                Assert.That(a.NomeAula, Is.EqualTo(aule[i].NomeAula));
                i++;
            }
        }
    }
}
