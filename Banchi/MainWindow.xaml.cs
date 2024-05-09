using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Label = System.Windows.Controls.Label;

namespace Banchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isDragging = false;
        private Point startPosition;

        Banco b; 
        public MainWindow()
        {
            InitializeComponent();

            // operazioni di inizializzazione da farsi per ogni banco
            // creazione dell'oggetto grafico che rappresenta il banco
            Label grafica = new Label();
            // creazione del banco, passando l'oggetto grafico
            // la classe Banco definirà l'aspetto e il comportamento del banco
            // il tavolo assume la sua posizione e dimensione di default
            b = new Banco(grafica, false);
            // aggiunta del banco all'area di disegno (Canvas)
            AreaDisegno.Children.Add(grafica);
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
                label.ReleaseMouseCapture();
            }
        }

        private void MenuAula_Click(object sender, RoutedEventArgs e)
        {
            AulaWindow wnd = new AulaWindow();
            wnd.Show();
        }

        // commentati, gli stessi metodi delegati, ma che funzionano nel Canvas
        ////
        ////Evento per iniziare il drag and drop
        //private void AreaDisegno_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (e.LeftButton == MouseButtonState.Pressed)
        //    {
        //        StackPanel stck = (StackPanel)sender;
        //        isDragging = true;
        //        startPosition = e.GetPosition(this);
        //        stck.CaptureMouse();
        //    }
        //}
        //// evento per continuare il drag and drop, quando il mouse si muove con il tasto premuto
        //private void Banco_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Point currentPosition = e.GetPosition(this);
        //    double offsetX = currentPosition.X - startPosition.X;
        //    double offsetY = currentPosition.Y - startPosition.Y;

        //    Canvas.SetLeft(Banco, Canvas.GetLeft(Banco) + offsetX);
        //    Canvas.SetTop(Banco, Canvas.GetTop(Banco) + offsetY);

        //    startPosition = currentPosition;
        //}
        //// evento per terminare il drag and drop, quando il tasto del mouse viene rilasciato
        //private void AreaDisegno_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (isDragging)
        //    {
        //        StackPanel stck = (StackPanel)sender;
        //        isDragging = false;
        //        stck.ReleaseMouseCapture();
        //    }
        //}
    }
}