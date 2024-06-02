using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Banchi
{
    public class Banco
    {
        // proprietà 
        public int CodiceBanco { get; set; }
        public bool IsCattedra { get; } = false;
        public static int NumeroBanchi { get; set; } = 1;
        public string NomeClasse { get; set; }
        public double AltezzaInCentimetri { get; set; }
        public double BaseInCentimetri { get; set; }

        private double posizioneXInPixel;
        private double posizioneYInPixel;

        private static double posizioneStartXInPixel = 0;
        private static double posizioneStartYInPixel = 0;
        // i banchi cambiano di dimensione quando si cambia la dimensione della finestra
        // (!!!! TODO vedere come si riposizionano !!!!)
        // fattore di scala moltiplicativo per il ridimensionamento, in [pixel/cm]
        private double fattoreDiScala = 0.1;
        public double PosizioneXInCentimetri { get; set; }
        public double PosizioneYInCentimetri { get; set; }

        private Label graficaBanco;
        private bool graficaInizializzata = false;


        public Label GraficaBanco
        {
            get
            {
                return graficaBanco;
            }
            set
            {
                graficaBanco = value;
                if (!graficaInizializzata)
                {
                    InizializzaGraficaBanco();
                }
            }
        }
        public double FattoreDiScala
        {
            get
            {
                return fattoreDiScala;
            }
            // quando cambia il fattore di scala cambiano le dimensioni del banco
            set
            {
                fattoreDiScala = value;
                // calcolo della posizione in pixel
                posizioneXInPixel = fattoreDiScala * PosizioneXInCentimetri;
                posizioneYInPixel = fattoreDiScala * PosizioneYInCentimetri;
                // impostazione della dimensione della grafica del banco
                GraficaBanco.Width = fattoreDiScala * BaseInCentimetri;
                GraficaBanco.Height = fattoreDiScala * AltezzaInCentimetri;
                // impostazione della posizione della grafica del banco
                Canvas.SetLeft(GraficaBanco, posizioneXInPixel);
                Canvas.SetTop(GraficaBanco, posizioneYInPixel);
            }
        }
        public Studente Studente { get; set; } = null;
        // il computer che (eventualmente) sta nel banco
        public Computer Computer { get; set; }
        // costruttore 
        public Banco(bool IsCattedra, double BaseInCentimetri, double AltezzaInCentimetri,
            double PosizioneXInCentimetri, double PosizioneYInCentimetri, Label GraficaBanco)
        {
            this.IsCattedra = IsCattedra;
            this.GraficaBanco = GraficaBanco;
            this.BaseInCentimetri = BaseInCentimetri;
            this.AltezzaInCentimetri = AltezzaInCentimetri;
            this.PosizioneXInCentimetri = PosizioneXInCentimetri;
            this.PosizioneYInCentimetri = PosizioneYInCentimetri;
            // la label viene passata dalla Window dove verrà disegnata
            // se c'è la inizializziamo 
            if (GraficaBanco != null)
                InizializzaGraficaBanco();
            CodiceBanco = NumeroBanchi;
            NumeroBanchi++;
        }
        private void InizializzaGraficaBanco()
        {
            // aspetto del banco 
            if (GraficaBanco != null)
            {
                graficaInizializzata = true;
                GraficaBanco.HorizontalContentAlignment = HorizontalAlignment.Center;
                GraficaBanco.VerticalContentAlignment = VerticalAlignment.Center;
                if (!IsCattedra)
                {
                    GraficaBanco.BorderThickness = new Thickness(2);
                    GraficaBanco.Background = Brushes.BurlyWood;
                }
                else
                {
                    GraficaBanco.BorderThickness = new Thickness(5);
                    GraficaBanco.Background = Brushes.PaleGreen;
                }
                GraficaBanco.BorderBrush = Brushes.Black;
                GraficaBanco.HorizontalAlignment = HorizontalAlignment.Left;
                GraficaBanco.VerticalAlignment = VerticalAlignment.Center;
                GraficaBanco.FontWeight = FontWeights.Bold;
                AggiungiTestoAGrafica();
                // posizione di default a tutti i banchi diversa,
                // in modo che non si sovrappongano completamente
                if (PosizioneXInCentimetri == null)
                {
                    posizioneXInPixel = posizioneStartXInPixel;
                    PosizioneXInCentimetri = posizioneStartXInPixel / fattoreDiScala;
                    posizioneStartXInPixel += 10;
                }
                else
                {
                    posizioneXInPixel = fattoreDiScala * PosizioneXInCentimetri;
                }
                if (PosizioneYInCentimetri == null)
                {
                    posizioneYInPixel = posizioneStartYInPixel;
                    PosizioneYInCentimetri = posizioneStartYInPixel / fattoreDiScala;
                    posizioneStartYInPixel += 10;
                }
                else
                {
                    posizioneYInPixel = fattoreDiScala * PosizioneYInCentimetri;
                }
                // impostazione della posizione della grafica del banco
                Canvas.SetLeft(GraficaBanco, posizioneXInPixel);
                Canvas.SetTop(GraficaBanco, posizioneYInPixel);
                // impostazione della dimensione della grafica del banco
                GraficaBanco.Width = fattoreDiScala * BaseInCentimetri;
                GraficaBanco.Height = fattoreDiScala * AltezzaInCentimetri;
            }
        }
        public void AggiungiTestoAGrafica()
        {
            Viewbox viewBoxLabel = new Viewbox()
            {
                StretchDirection = StretchDirection.Both
            };
            GraficaBanco.Content = viewBoxLabel;
            TextBlock tb = new TextBlock();
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.TextAlignment = TextAlignment.Center;
            tb.TextWrapping = TextWrapping.WrapWithOverflow;
            string testo;
            if (Computer != null && Computer.NomeDispositivo != null)
                testo = Computer.NomeDispositivo;
            else
                testo = "---";
            testo += "\n-----\n";
            if (Studente != null && Studente.Cognome != null)
                testo += Studente.Cognome;
            else
                testo += "---";
            if (Studente != null && Studente.Nome != null)
                testo += "\n" + Studente.Nome;
            else
                testo += "\n---";
            tb.Inlines.Add(testo);

            viewBoxLabel.Child = tb;
        }
    }
}
