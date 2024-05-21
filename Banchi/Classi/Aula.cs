using Banchi.Classi;
using System.Windows;
using System.Windows.Controls;

namespace Banchi
{
    public class Aula
    {
        public string NomeAula { get; set; }
        // dimensioni dell'aula. 
        public double AltezzaInCentimetri { get; set; }
        public double BaseInCentimetri { get; set; }
        // ci serve la lista dei banchi che stanno in questa aula
        public List<Banco> Banchi { get; set; }
        // NON serve la lista dei computer che stanno in questa aula
        // perchè ogni banco può avere il suo computer

        // per disegnare le porte e le finestre dell'aula
        // definiano la struttura dati dei serramnti
        public List<Serramento> Serramenti { get; set; }
        // angolo del Nord rispetto al lato 1, in gradi
        public int? DirezioneNord { get; set; }

        // forse ci servirà la lista degli studenti che stanno in questa aula
        //public List<Studente> Studenti { get; set; } // eliminarla se poi non serve
        public Aula(string NomeAula, double AltezzaInCentimetri, double BaseInCentimetri, 
            int? DirezioneNord = null)
        {
            // inizializzazione delle proprietà
            this.AltezzaInCentimetri = AltezzaInCentimetri;
            this.BaseInCentimetri = BaseInCentimetri;
            this.NomeAula = NomeAula;
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
