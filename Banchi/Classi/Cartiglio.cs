using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Banchi.Classi
{
    internal class Cartiglio
    {
        public string Aula { get; set; }
        public string Classe { get; set; }
        public string Utente { get; set; }
        public Label GraficaCartiglio { get; set; }
        public Cartiglio(Label graficaCartiglio, Aula aula, Classe classe, string utente)
        {
            GraficaCartiglio = graficaCartiglio;
            if (aula != null && aula.NomeAula != null)
                Aula = aula.NomeAula;
            else
                Aula = "++++";
            if (classe != null && classe.CodiceClasse != null)
                Classe = classe.CodiceClasse;
            else
                Classe = "++++";
            Utente = utente;
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
