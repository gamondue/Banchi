using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Banchi.Classi
{
    public class RosaDeiVenti
    {
        public double Angolo { get; set; } = 0;
        double fattoreDiScala = 0.1;

        public double PosizioneXInCentimetri { get; set; }
        public double PosizioneYInCentimetri { get; set; }
        double posizioneXInPixel;
        double posizioneYInPixel;
        private Image graficaRosaDeiVenti;
        private double BaseInCentimetri = 70;
        private double AltezzaInCentimetri = 70;

        public RosaDeiVenti(double angolo, Image graficaRosaDeiVenti = null)
        {
            this.graficaRosaDeiVenti = graficaRosaDeiVenti;
            this.Angolo = angolo;
            if (graficaRosaDeiVenti != null)
            {
                Uri resourceUri = new Uri("pack://application:,,,/Immagini/RosaDeiVenti.png", UriKind.Absolute);
                BitmapImage bitmapImage = new BitmapImage(resourceUri);
                graficaRosaDeiVenti.Source = bitmapImage;
            }
        }
        public double FattoreDiScala
        {
            get
            {
                return fattoreDiScala;
            }
            // quando cambia il fattore di scala cambiano le dimensioni della rosa dei venti
            set
            {
                //if (graficaRosaDeiVenti == null)
                //{
                //    graficaRosaDeiVenti = new(); 
                //    Uri resourceUri = new Uri("pack://application:,,,/Immagini/RosaDeiVenti.png", UriKind.Absolute);
                //    BitmapImage bitmapImage = new BitmapImage(resourceUri);
                //    graficaRosaDeiVenti.Source = bitmapImage;
                //}
                fattoreDiScala = value;
                // calcolo della posizione in pixel
                posizioneXInPixel = fattoreDiScala * PosizioneXInCentimetri;
                posizioneYInPixel = fattoreDiScala * PosizioneYInCentimetri;
                if (graficaRosaDeiVenti != null)
                {
                    // impostazione della dimensione della graficaAula della rosa dei venti
                    graficaRosaDeiVenti.Width = fattoreDiScala * BaseInCentimetri;
                    graficaRosaDeiVenti.Height = fattoreDiScala * AltezzaInCentimetri;
                    // impostazione della posizione della graficaAula della rosa dei venti
                    Canvas.SetLeft(graficaRosaDeiVenti, posizioneXInPixel);
                    Canvas.SetTop(graficaRosaDeiVenti, posizioneYInPixel);

                    // ruota e scala l'immagine
                    // Crea un TransformGroup per combinare le trasformazioni
                    TransformGroup transformGroup = new TransformGroup();
                    // Aggiungi la trasformazione di rotazione
                    RotateTransform rotateTransform = new RotateTransform(Angolo); // Angolo di rotazione in gradi
                    transformGroup.Children.Add(rotateTransform);
                    // Aggiungi la trasformazione di scalatura
                    // Fattore di scala 1 = non scala (già fatto a mano)
                    ScaleTransform scaleTransform = new ScaleTransform(1, 1);
                    transformGroup.Children.Add(scaleTransform);
                    // Applica le trasformazioni all'immagine
                    graficaRosaDeiVenti.RenderTransform = transformGroup;
                }
            }
        }
    }
}
