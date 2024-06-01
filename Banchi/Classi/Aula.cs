using Banchi.Classi;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Banchi
{
    public class Aula
    {
        public string NomeAula { get; set; }
        // dimensioni dell'aula
        public double AltezzaInCentimetri { get; set; }
        public double BaseInCentimetri { get; set; }
        public int? DirezioneNord { get; set; }
        // la classe che usa questa aula, non si usa sempre 
        public Classe Classe { get; set; }

        private bool graficaInizializzata = false;
        private Label graficaAula;
        public Label GraficaAula
        {
            get
            {
                return graficaAula;
            }
            set
            {
                graficaAula = value;
                if (!graficaInizializzata)
                {
                    InizializzaGraficaAula();
                }
            }
        }

        // fattore di scala moltiplicativo, in [pixel/cm]
        public double FattoreDiScala = 0.1;
        // ci serve la lista dei banchi che stanno in questa aula
        public List<Banco> Banchi { get; set; }
        // NON serve la lista dei computer che stanno in questa aula, da esporre come proprietà 
        // perché i computer stanno sui banchi ed ogni banco può avere il suo computer

        // per disegnare le porte e le finestre dell'aula
        // definiamo la struttura dati dei serramenti
        public List<Serramento> Serramenti { get; set; }
        // angolo del Nord rispetto al lato 1, in gradi

        public Aula(string NomeAula, double AltezzaInCentimetri, double BaseInCentimetri,
            Label GraficaAula = null, int? DirezioneNord = null)
        {
            // inizializzazione delle proprietà
            this.AltezzaInCentimetri = AltezzaInCentimetri;
            this.BaseInCentimetri = BaseInCentimetri;
            this.NomeAula = NomeAula;

            if (GraficaAula != null)
            {
                InizializzaGraficaAula();
            }
            if (DirezioneNord != null)
                this.DirezioneNord = DirezioneNord;
            Banchi = new List<Banco>();
            Serramenti = new List<Serramento>();
        }
        private void InizializzaGraficaAula()
        {
            if (GraficaAula != null)
            {
                graficaInizializzata = true;
                // aspetto dell'aula
                this.GraficaAula = GraficaAula;
                GraficaAula.Height = FattoreDiScala * AltezzaInCentimetri;
                GraficaAula.Width = FattoreDiScala * BaseInCentimetri;
                GraficaAula.BorderThickness = new Thickness(12);
                GraficaAula.BorderBrush = Brushes.Black;
                GraficaAula.HorizontalAlignment = HorizontalAlignment.Left;
                GraficaAula.VerticalAlignment = VerticalAlignment.Center;
                GraficaAula.Background = Brushes.LightGray;
            }
        }
        public void MettiInScalaAulaEBanchi()
        {
            MettiInScalaAula();
            MettiInScalaBanchi();
        }
        private void MettiInScalaAula()
        {
            // facciamo in modo che il lato più lungo stia lungo le X 
            double temp;
            if (AltezzaInCentimetri > BaseInCentimetri)
            {
                // scambio fra base ed altezza 
                temp = BaseInCentimetri;
                BaseInCentimetri = AltezzaInCentimetri;
                AltezzaInCentimetri = temp;
            }
            // acquisisco la finestra dove c'è la Label che rappresenta l'aula
            Window finestraContenitore = Window.GetWindow(GraficaAula);
            // il fattore di scala è il rapporto fra le dimensioni in pixel della Window
            // e la dimensione in centimetri dell'aula
            // tolgo ai pixel nelle Y il numero di pixel delle barre superiori 
            double fattoreDiScalaY = (finestraContenitore.ActualHeight - 193) / AltezzaInCentimetri;
            double fattoreDiScalaX = finestraContenitore.ActualWidth / BaseInCentimetri;
            // il fattore di scala che adotto per i disegno è il più piccolo dei due, così ci sta tutto 
            if (fattoreDiScalaX > fattoreDiScalaY)
            {
                FattoreDiScala = fattoreDiScalaY;
            }
            else
            {
                FattoreDiScala = fattoreDiScalaX;
            }
            GraficaAula.Height = FattoreDiScala * AltezzaInCentimetri;
            GraficaAula.Width = FattoreDiScala * BaseInCentimetri;
            // l'aula ha sempre posizione 0,0
        }
        private void MettiInScalaBanchi()
        {
            //  cambia il FattoreDiScala ad ogni banco, per visualizzarlo nelle giuste dimensioni
            // e nel giusto posto, prende lo stesso fattore di scala dell'aula
            foreach (Banco b in Banchi)
            {
                b.FattoreDiScala = FattoreDiScala;
            }
        }
        public override string ToString()
        {
            // per riempire il ComboBox con il nome dell'aula
            if (Classe == null)
                return NomeAula;
            else
                return NomeAula + " " + Classe.CodiceClasse;
        }
    }
}
