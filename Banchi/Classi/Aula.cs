using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Banchi
{
    internal class Aula
    {
        public string NomeAula { get; set; }
        // dimensioni dell'aula. 
        public double BaseInCentimetri { get; set; }
        public double AltezzaInCentimetri { get; set; }
        // ci serve la lista dei banchi che stanno in questa aula
        public List<Banco> Banchi { get; set; }
        // ci serve la lista dei computer che stanno in questa aula
        public List<Computer> Computer { get; set; }
        Label GraficaAula { get; set; }

        // vogliamo disegnare le porte e le finestre dell'aula
        // !!!! qui metteremo le strutture dati che serviranno per disegnare le porte e le finestre !!!!

        // forse ci servirà la lista degli studenti che stanno in questa aula
        //public List<Studente> Studenti { get; set; } // eliminarla se poi non serve
        public Aula(string NomeAula, double AltezzaInCentimetri, double BaseInCentimetri, Label GraficaAula)
        {
            // inizializzazione delle proprietà
            this.AltezzaInCentimetri = AltezzaInCentimetri;
            this.BaseInCentimetri = BaseInCentimetri;
            this.NomeAula = NomeAula;
            this.GraficaAula = GraficaAula;
            // aspetto del banco, DA MIGLIORARE! 
            //GraficaAula.HorizontalContentAlignment = HorizontalAlignment.Center;
            //GraficaAula.VerticalContentAlignment = VerticalAlignment.Center;
            GraficaAula.BorderThickness = new Thickness(2);
            GraficaAula.BorderBrush = Brushes.Black;
            GraficaAula.HorizontalAlignment = HorizontalAlignment.Left;
            GraficaAula.VerticalAlignment = VerticalAlignment.Center;
            GraficaAula.Background = Brushes.LightGray;
            //GraficaAula.FontWeight = FontWeights.Bold;

            Banchi = new List<Banco>();
            Computer = new List<Computer>();
            //Studenti = new List<Studente>();
        }
        public override string ToString()
        {
            return NomeAula; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
