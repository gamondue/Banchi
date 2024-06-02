using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Banchi.Classi
{
    public class Cartiglio
    {
        public string Aula { get; set; }
        public string Classe { get; set; }
        public string Utente { get; set; }
        double fattoreDiScala = 0.1;
        public double PosizioneXInCentimetri { get; set; }
        public double PosizioneYInCentimetri { get; set; }
        public double BaseInCentimetri { get; set; } = 180;
        public double AltezzaInCentimetri { get; set; } = 60;
        private bool graficaInizializzata = false;
        private double posizioneXInPixel;
        private double posizioneYInPixel;
        private Label graficaCartiglio;
        public Label GraficaCartiglio
        {
            get
            {
                return graficaCartiglio;
            }
            set
            {
                graficaCartiglio = value;
                if (!graficaInizializzata)
                {
                    InizializzaGraficaCartiglio();
                }
            }
        }
        public double FattoreDiScala
        {
            get
            {
                return fattoreDiScala;
            }
            // quando cambia il fattore di scala cambiano le dimensioni del cartiglio
            set
            {
                fattoreDiScala = value;
                // calcolo della posizione in pixel
                posizioneXInPixel = fattoreDiScala * PosizioneXInCentimetri;
                posizioneYInPixel = fattoreDiScala * PosizioneYInCentimetri;
                // impostazione della dimensione della grafica del cartiglio
                graficaCartiglio.Width = fattoreDiScala * BaseInCentimetri;
                graficaCartiglio.Height = fattoreDiScala * AltezzaInCentimetri;
                // impostazione della posizione della grafica del cartiglio
                Canvas.SetLeft(graficaCartiglio, posizioneXInPixel);
                Canvas.SetTop(graficaCartiglio, posizioneYInPixel);
            }
        }
        public Cartiglio(Aula aula, Classe classe, string utente, Label GraficaCartiglio)
        {
            this.graficaCartiglio = GraficaCartiglio;

            if (aula != null && aula.NomeAula != null)
                Aula = aula.NomeAula;
            else
                Aula = "++++";
            if (classe != null && classe.CodiceClasse != null)
                Classe = classe.CodiceClasse;
            else
                Classe = "++++";
            Utente = utente;

            posizioneXInPixel = 20;
            posizioneYInPixel = 20;

            InizializzaGraficaCartiglio();
            fattoreDiScala = 1;
        }
        private void InizializzaGraficaCartiglio()
        {
            if (GraficaCartiglio != null)
            {
                graficaInizializzata = true;
                GraficaCartiglio.HorizontalContentAlignment = HorizontalAlignment.Center;
                GraficaCartiglio.VerticalContentAlignment = VerticalAlignment.Center;
                GraficaCartiglio.BorderThickness = new Thickness(1);
                GraficaCartiglio.BorderBrush = Brushes.Black;
                GraficaCartiglio.HorizontalAlignment = HorizontalAlignment.Left;
                GraficaCartiglio.VerticalAlignment = VerticalAlignment.Center;
                GraficaCartiglio.Background = Brushes.White;
                GraficaCartiglio.FontWeight = FontWeights.DemiBold;

                // il cartiglio sta sopra a tutto il resto
                Panel.SetZIndex(GraficaCartiglio, 5000);

                AggiungiTestoAGrafica();

                // impostazione della posizione della grafica del banco
                Canvas.SetLeft(GraficaCartiglio, posizioneXInPixel);
                Canvas.SetTop(GraficaCartiglio, posizioneYInPixel);
                // impostazione della dimensione della grafica del banco
                GraficaCartiglio.Width = FattoreDiScala * BaseInCentimetri;
                GraficaCartiglio.Height = FattoreDiScala * AltezzaInCentimetri;
            }
        }
        public void AggiungiTestoAGrafica()
        {
            Viewbox viewBoxLabel = new Viewbox()
            {
                StretchDirection = StretchDirection.Both
            };
            GraficaCartiglio.Content = viewBoxLabel;
            TextBlock tb = new TextBlock();
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.TextAlignment = TextAlignment.Center;
            tb.TextWrapping = TextWrapping.WrapWithOverflow;
            string testo = "Aula: " + Aula.ToString();
            //testo += "\n-----\n";
            testo += "\nClasse: " + Classe.ToString();
            testo += "\n" + Utente;
            tb.Inlines.Add(testo);

            viewBoxLabel.Child = tb;
        }
    }
}
