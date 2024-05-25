using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per BanchiWindow.xaml
    /// </summary>
    public partial class BanchiWindow : Window
    {
        bool isDragging = false;
        private Point startPosition;

        Banco banco;
        List<Banco> banchi = new();

        public BanchiWindow()
        {
            InitializeComponent();
            //BusinessLayer.LeggiBanchi();
        }
        private void btn_NuovoBanco_Click(object sender, RoutedEventArgs e)
        {
            // operazioni di inizializzazione da farsi per ogni banco che voglio creare
            // creazione dell'oggetto grafico che rappresenta il banco
            Label grafica = new Label();
            // creazione del banco, passando l'oggetto grafico
            // listaAule classe Banco definirà l'aspetto e il comportamento del banco
            // il tavolo assume listaAule sua posizione e dimensione di default
            double larghezza = Convert.ToDouble(txt_Larghezza.Text);
            double lunghezza = Convert.ToDouble(txt_Lunghezza.Text);
            Size misure = new Size(larghezza, lunghezza);
            banco = new Banco(grafica, false, misure, Banco.posizioneIniziale);
            // aggiunta del banco all'area di disegno (Canvas)
            AreaDisegno.Children.Add(banco.GraficaBanco);
            //aggiungere banco a lista
            banchi.Add(banco);
            // metodi delegati per gestione drag and drop
            grafica.MouseDown += ClickSuBanco;
            grafica.MouseMove += MovimentoSuBanco;
            grafica.MouseUp += MouseUpSuBanco;
        }

        // implementazione dei metodi delegati per gestione drag and drop
        // evento per iniziare il drag and drop, quando l'utente clicca sul banco
        // è necesssario scrivere questi metodi qui, e non nella classe Banco,
        // perchè il drag and drop è un'operazione che coinvolge l'interfaccia grafica
        // e fare in modo che lo possa fare Banco è complicaato
        internal void ClickSuBanco(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Label daPrendere = (Label)sender;
                isDragging = true;
                startPosition = e.GetPosition((IInputElement)this);
                daPrendere.CaptureMouse();
            }
        }
        // evento per continuare il drag and drop, quando il mouse si muove con il tasto premuto
        internal void MovimentoSuBanco(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Label label = (Label)sender;
            if (isDragging)
            {
                Point currentPosition = e.GetPosition((IInputElement)this);
                double offsetX = currentPosition.X - startPosition.X;
                double offsetY = currentPosition.Y - startPosition.Y;

                Canvas.SetLeft(label, Canvas.GetLeft(label) + offsetX);
                Canvas.SetTop(label, Canvas.GetTop(label) + offsetY);

                startPosition = currentPosition;
            }
        }
        // evento per terminare il drag and drop, quando il tasto del mouse viene rilasciato
        internal void MouseUpSuBanco(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            if (isDragging)
            {
                isDragging = false;
                startPosition = e.GetPosition((IInputElement)this);
                TextBlock textBlock1 = (TextBlock)label.Content;
                string contenuto=textBlock1.Inlines.ToString();
                contenuto.Split('\n');
                foreach (Banco b in banchi)
                {
                    TextBlock textBlockB = (TextBlock)label.Content;
                    string contenutoB = textBlockB.Text;
                    contenutoB.Split('\n');
                    if (contenuto[0] == contenutoB[0])
                    {
                        b.Position = startPosition;
                    }
                }
                label.ReleaseMouseCapture();
            }
        }

        private void btn_SalvataggioBanchi_Click(object sender, RoutedEventArgs e)
        {
            //BusinessLayer.ScriviTuttiIBanchi();
        }

        private void btn_NuovaCattedra_Click(object sender, RoutedEventArgs e)
        {
            // operazioni di inizializzazione da farsi per ogni banco che voglio creare
            // creazione dell'oggetto grafico che rappresenta il banco
            Label grafica = new Label();
            // creazione del banco, passando l'oggetto grafico
            // listaAule classe Banco definirà l'aspetto e il comportamento del banco
            // il tavolo assume listaAule sua posizione e dimensione di default
            double larghezza = Convert.ToDouble(txt_Larghezza.Text);
            double lunghezza = Convert.ToDouble(txt_Lunghezza.Text);
            Size misure = new Size(larghezza, lunghezza);
            banco = new Banco(grafica, true, misure, Banco.posizioneIniziale);
            // aggiunta del banco all'area di disegno (Canvas)
            AreaDisegno.Children.Add(grafica);
            // metodi delegati per gestione drag and drop
            grafica.MouseDown += ClickSuBanco;
            grafica.MouseMove += MovimentoSuBanco;
            grafica.MouseUp += MouseUpSuBanco;
        }
    }
}
