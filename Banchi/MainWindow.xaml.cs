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
        Banco banco; 
        public MainWindow()
        {
            InitializeComponent();
            // legge tutte le aule dal "database" e le mette in una lista
            List<Aula> listaAule = BusinessLayer.LeggiTutteLeAule();
            // riempimento del ComboBox con le aule appena lette
            foreach (Aula a in listaAule)
            {
                // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                // devo implementare il metodo ToString() dentro listaAule classe Aule
                cmbAule.Items.Add(a);
            }
            // esempi di inizializzazione di un ComboBox
            cmbAule.SelectedIndex = 1; // seleziona listaAule seconda aula
            cmbAule.SelectedItem = cmbAule.Items[1]; // seleziona listaAule seconda aula
            // recupera il nome dell'aula selezionata 
            //string aulaSelezionata = ((Aula)cmbAule.SelectedItem).NomeAula;
            // recupera l'altezza dell'aula selezionata
            //double altezzaAula = ((Aula)cmbAule.SelectedItem).AltezzaInMetri;
            //List<Classe> listaClassi=BusinessLayer.LeggiTutteLeClassi();

            //foreach(Classe a in listaClassi)
            //{
            //    cmb_Classe.Items.Add(a);
            //}
            //cmb_Classe.SelectedIndex = 1;
            //cmb_Classe.SelectedItem = cmb_Classe.Items[1];           
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
            banco = new Banco(grafica, false, misure);
            // aggiunta del banco all'area di disegno (Canvas)
            AreaDisegno.Children.Add(grafica);
            // metodi delegati per gestione drag and drop
            grafica.MouseDown += ClickSuBanco;
            grafica.MouseMove += MovimentoSuBanco;
            grafica.MouseUp += MouseUpSuBanco;
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow wnd = new AboutWindow();
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