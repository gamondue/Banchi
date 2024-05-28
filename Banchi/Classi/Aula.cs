using Banchi.Classi;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Banchi
{
    public class Aula
    {
        public string NomeAula { get; set; }
        public Label GraficaAula { get; }
        // dimensioni dell'aula. 
        public double AltezzaInCentimetri { get; set; }
        public double BaseInCentimetri { get; set; }
        // ci serve la lista dei banchi che stanno in questa aula
        public List<Banco> Banchi { get; set; }
        // NON serve la lista dei computer che stanno in questa aula
        // perchè ogni banco può avere il suo computer
        Label graficaAula { get; set; }
        // per disegnare le porte e le finestre dell'aula
        // definiamo la struttura dati dei serramenti
        public List<Serramento> Serramenti { get; set; }
        // angolo del Nord rispetto al lato 1, in gradi
        public int? DirezioneNord { get; set; } //293 FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF

        // forse ci servirà la lista degli studenti che stanno in questa aula
        //public List<Studente> Studenti { get; set; } // eliminarla se poi non serve
        public Aula(string NomeAula, double AltezzaInCentimetri, double BaseInCentimetri,
            int? DirezioneNord = null, Label GraficaAula = null)
        {


            // inizializzazione delle proprietà
            this.AltezzaInCentimetri = AltezzaInCentimetri;
            this.BaseInCentimetri = BaseInCentimetri;
            this.NomeAula = NomeAula;

            if (GraficaAula != null)
            {
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
            }

            if (DirezioneNord != null)
                this.DirezioneNord = DirezioneNord;
            Banchi = new List<Banco>();
            Serramenti = new List<Serramento>();

        }
        public void VisualizzaAulaEBanchi()
        {
            VisualizzaAula();
            VisualizzaBanchi();
        }
        private void VisualizzaAula()
        {
            // !!!! TODO DA FARE !!!!
            //throw new NotImplementedException();
        }
        // temporaneo
        Banco tempBanco;
        private void VisualizzaBanchi()
        {
            // !!!! PEZZO DA FARE TUTTO !!!!
        }
        public override string ToString()
        {
            return NomeAula; // per riempire il ComboBox con il nome dell'aula
        }
    }
}
