using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Banchi.Classi
{
    internal class Cartiglio
    {
        public string Aula { get; set; }
        public string Classe { get; set; }
        public string Utente { get; set; }
        double fattoreDiScala = 0.1;
        public double PosizioneXInCentimetri { get; set; }
        public double PosizioneYInCentimetri { get; set; }
        private static double posizioneStartX = 0;
        private static double posizioneStartY = 0;
        public double BaseInCentimetri = 180;
        public double AltezzaInCentimetri = 60;
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
            // quando cambia il fattore di scala cambiano le dimensioni del banco
            set
            {
                fattoreDiScala = value;
                GraficaCartiglio.Height = fattoreDiScala * AltezzaInCentimetri;
                GraficaCartiglio.Width = fattoreDiScala * BaseInCentimetri;
            }
        }
        public Cartiglio(Label graficaCartiglio, Aula aula, Classe classe, string utente)
        {
            //GraficaCartiglio = graficaCartiglio;

            if (aula != null && aula.NomeAula != null)
                Aula = aula.NomeAula;
            else
                Aula = "++++";
            if (classe != null && classe.CodiceClasse != null)
                Classe = classe.CodiceClasse;
            else
                Classe = "++++";
            Utente = utente;

            if (PosizioneXInCentimetri == null || PosizioneXInCentimetri == 0)
            {
                posizioneXInPixel = posizioneStartX;
                posizioneStartX += 10;
            }
            else
            {
                posizioneXInPixel = aula.FattoreDiScala * PosizioneXInCentimetri;
            }
            if (PosizioneYInCentimetri == null || PosizioneYInCentimetri == 0)
            {
                posizioneYInPixel = posizioneStartY;
                posizioneStartY += 10;
            }
            else
            {
                posizioneYInPixel = aula.FattoreDiScala * PosizioneYInCentimetri;
            }
            // impostazione della posizione della grafica del banco
            Canvas.SetLeft(graficaCartiglio, posizioneXInPixel);
            Canvas.SetTop(graficaCartiglio, posizioneYInPixel);
            // impostazione della dimensione della grafica del banco
            graficaCartiglio.Width = aula.FattoreDiScala * BaseInCentimetri;
            graficaCartiglio.Height = aula.FattoreDiScala * AltezzaInCentimetri;
        }

        private void InizializzaGraficaCartiglio()
        {     
            if (GraficaCartiglio != null)
            {
                graficaInizializzata = true;
                //GraficaCartiglio = graficaCartiglio;
                GraficaCartiglio.HorizontalContentAlignment = HorizontalAlignment.Center;
                GraficaCartiglio.VerticalContentAlignment = VerticalAlignment.Center;
                GraficaCartiglio.BorderThickness = new Thickness(1);
                GraficaCartiglio.BorderBrush = Brushes.Black;
                GraficaCartiglio.HorizontalAlignment = HorizontalAlignment.Left;
                GraficaCartiglio.VerticalAlignment = VerticalAlignment.Center;
                GraficaCartiglio.Background = Brushes.White;
                GraficaCartiglio.FontWeight = FontWeights.DemiBold;

                GraficaCartiglio.Height = 60;
                GraficaCartiglio.Width = 180;
                Panel.SetZIndex(GraficaCartiglio, 5000);
                AggiungiTestoAGrafica();
                // posizione di default a tutti i banchi diversa,
                // in modo che non si sovrappongano completamente

                if (PosizioneXInCentimetri == null || PosizioneXInCentimetri == 0)
                {
                    posizioneXInPixel = posizioneStartX;
                    posizioneStartX += 10;
                }
                else
                {
                    posizioneXInPixel = FattoreDiScala * PosizioneXInCentimetri;
                }
                if (PosizioneYInCentimetri == null || PosizioneYInCentimetri == 0)
                {
                    posizioneYInPixel = posizioneStartY;
                    posizioneStartY += 10;
                }
                else
                {
                    posizioneYInPixel = FattoreDiScala * PosizioneYInCentimetri;
                }
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
            
            TextBlock tb = new TextBlock();
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.TextAlignment = TextAlignment.Center;
            tb.TextWrapping = TextWrapping.WrapWithOverflow;
            tb.Inlines.Add("Aula: " + Aula);
            tb.Inlines.Add(new LineBreak());
            //tb.Inlines.Add(new Run("-----"));
            //tb.Inlines.Add(new LineBreak());
            tb.Inlines.Add("Classe: " + Classe);
            tb.Inlines.Add(new LineBreak());
            //tb.Inlines.Add(new Run("-----"));
            //tb.Inlines.Add(new LineBreak());
            tb.Inlines.Add(Utente);
            GraficaCartiglio.Content = tb;
            Canvas.SetLeft(GraficaCartiglio, 10);
            Canvas.SetTop(GraficaCartiglio, 10);
            
        }
    }
}
