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
        private Cartiglio cartiglio;
        public Cartiglio Cartiglio
        {
            get
            {
                return cartiglio;
            }
            set
            {
                cartiglio = value;
                if (cartiglio != null)
                {
                    cartiglio.FattoreDiScala = FattoreDiScala;
                }
            }
        }
        private RosaDeiVenti rosaDeiVenti;
        public RosaDeiVenti RosaDeiVenti
        {
            get
            {
                return rosaDeiVenti;
            }
            set
            {
                rosaDeiVenti = value;
                if (rosaDeiVenti != null)
                {
                    rosaDeiVenti.FattoreDiScala = FattoreDiScala;
                }
            }
        }
        public double? RosaDeiVentiX { get; set; }
        public double? RosaDeiVentiY { get; set; }

        private bool graficaInizializzata = false;
        private bool rosaDeiVentiInizializzata = false;
        private Label graficaAula;
        // GraficaAula è la Label che rappresenta l'aula nella finestra WPF
        // la variabile incapsulata graficaAula viene gestita automaticamente 
        // quando si cambia la proprietà GraficaAula
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
        private Image graficaRosaDeiVenti;
        public Image GraficaRosaDeiVenti
        {
            get
            {
                return graficaRosaDeiVenti;
            }
            set
            {
                graficaRosaDeiVenti = value;
                if (!rosaDeiVentiInizializzata)
                {
                    rosaDeiVenti = new RosaDeiVenti((double)DirezioneNord, graficaRosaDeiVenti);
                    rosaDeiVentiInizializzata = true;
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
            double? direzioneNord = null, double? rosaDeiVentiX = null, double? rosaDeiVentiY = null,
            Label graficaAula = null, Image graficaRosaDeiVenti = null)
        {
            // inizializzazione delle proprietà
            this.AltezzaInCentimetri = AltezzaInCentimetri;
            this.BaseInCentimetri = BaseInCentimetri;
            this.NomeAula = NomeAula;

            if (graficaAula != null)
            {
                InizializzaGraficaAula();
            }
            if (direzioneNord != null)
            {
                this.DirezioneNord = (int)direzioneNord;
                this.graficaRosaDeiVenti = graficaRosaDeiVenti;
                RosaDeiVenti = new RosaDeiVenti((double)DirezioneNord, graficaRosaDeiVenti);
                if (rosaDeiVentiX != null && rosaDeiVentiY != null)
                {
                    RosaDeiVentiX = rosaDeiVentiX;
                    RosaDeiVentiY = rosaDeiVentiY;
                }
            }
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
            if (cartiglio != null)
                cartiglio.FattoreDiScala = FattoreDiScala;
            if (rosaDeiVenti != null)
                rosaDeiVenti.FattoreDiScala = FattoreDiScala;
        }
        private void MettiInScalaAula()
        {
            double temp;
            // nella seguente if, se mettiamo il segno > il lato più lungo sta lungo le X 
            // della UI, se mettiamo il segno <, è il lato più corto che sta lungo le X 
            if (AltezzaInCentimetri < BaseInCentimetri)
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
            double fattoreDiScalaY = (finestraContenitore.ActualHeight - 204) / AltezzaInCentimetri;
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
            // cambia il FattoreDiScala ad ogni banco, per visualizzarlo nelle giuste dimensioni
            // e nel giusto posto, prende lo stesso fattore di scala dell'aula
            foreach (Banco b in Banchi)
            {
                b.FattoreDiScala = FattoreDiScala;
            }
        }
        public override string ToString()
        {
            // per riempire il controllo WPF con il nome dell'aula
            if (Classe == null)
                return NomeAula;
            else
                return NomeAula + " " + Classe.CodiceClasse;
        }
    }
}
