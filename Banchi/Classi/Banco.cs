
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Banchi
{
    public class Banco
    {
        // proprietà 
        public int CodiceBanco { get; set; }
        public string NomeClasse { get; set; }
        public Point Position { get; set; }
        public Size Size { get; set; }
        public static Point posizioneIniziale { get; set; } = new Point(0, 0);
        Label GraficaBanco { get; set; }
        public bool IsCattedra { get; } = false;
        public string CognomeNomeStudente { get; set; }
        // il computer che (eventualmente) sta nel banco
        public Computer Computer { get; set; }
        // costruttore 
        public Banco(Label GraficaBanco, bool IsCattedra, Size misure)
        {
            this.IsCattedra = IsCattedra;
            // la label viene passata dalla Window, dove verrà disegnata
            this.GraficaBanco = GraficaBanco;
            // aspetto del banco, DA MIGLIORARE! 
            GraficaBanco.HorizontalContentAlignment = HorizontalAlignment.Center;
            GraficaBanco.VerticalContentAlignment = VerticalAlignment.Center;
            if (!IsCattedra)
            {
                GraficaBanco.BorderThickness = new Thickness(2);
            }
            else
            {
                GraficaBanco.BorderThickness = new Thickness(4);
            }
            GraficaBanco.BorderBrush = Brushes.Black;
            GraficaBanco.HorizontalAlignment = HorizontalAlignment.Left;
            GraficaBanco.VerticalAlignment = VerticalAlignment.Center;
            GraficaBanco.Background = Brushes.BurlyWood;
            GraficaBanco.FontWeight = FontWeights.Bold;
            GraficaBanco.Content = CodiceBanco + "\n" + "_____________" + "\n" + CognomeNomeStudente;
            // posizione di default (darla a tutti i banchi diversa,
            // in modo che non si sovrappongano completamente)
            this.Position = posizioneIniziale;
            posizioneIniziale = new Point(posizioneIniziale.X + 10, posizioneIniziale.Y + 10);
            // dimensione di default, vedere quale è la migliore
            // quando il resto funzione, si può pensare di poter rendere le proporzioni dei banchi 
            // configurabili dall'utente, in modo che possano essere come i banchi "veri" 
            // questo potrebbe essere fatto chiedendo altezza e larghezza (in cm, da convertire in pixel)
            // MA SOLO DOPO CHE IL RESTO FUNZIONA
            // impostazione della posizione della grafica del banco
            Canvas.SetLeft(GraficaBanco, this.Position.X);
            Canvas.SetTop(GraficaBanco, this.Position.Y);
            // impostazione della dimensione della grafica del banco
            this.Size = misure; // dimensione di default
            GraficaBanco.Width = this.Size.Width;
            GraficaBanco.Height = this.Size.Height;
        }
        // sarebbe bello che i banchi cambiassero di dimensione quando
        // si cambia la dimensione della finestra e si riposizionassero automaticamente 
    }
}
