using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Label = System.Windows.Controls.Label;

namespace Banchi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BusinessLayer.Inizializzazioni();

            // legge tutte le aule dal "database" e le mette in una lista
            List<Aula> listaAuleUtente = BusinessLayer.LeggiTutteLeAuleUtente();
            // riempimento del ComboBox con le aule appena lette
            if (listaAuleUtente != null)
                foreach (Aula a in listaAuleUtente)
                {
                    // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                    // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                    cmbAuleUtente.Items.Add(a);
                }

            List<Classe> listaClassiUtente = BusinessLayer.LeggiTutteLeClassiUtente();
            if (listaAuleUtente != null)
                foreach (Classe a in listaClassiUtente)
                {
                    cmbClasseUtente.Items.Add(a);
                }
            // riempio i combobox dei modelli
            List<Aula> listaAuleModello = BusinessLayer.LeggiTutteLeAule();
            // riempimento del ComboBox con le aule appena lette
            foreach (Aula a in listaAuleModello)
            {
                // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                cmbModelliAule.Items.Add(a);
            }
            // esempi di inizializzazione di un ComboBox
            //cmbModelliAule.SelectedIndex = 1; // seleziona listaAuleUtente seconda aula
            //cmbModelliAule.SelectedItem = cmbAuleUtente.Items[1]; // seleziona listaAuleUtente seconda aula
            // recupera il nome dell'aula selezionata 
            //string aulaSelezionata = ((Aula)cmbAule.SelectedItem).NomeAula;
            // recupera l'altezza dell'aula selezionata
            //double altezzaAula = ((Aula)cmbAule.SelectedItem).AltezzaInCentimetri;
            List<Classe> listaClassiModello = BusinessLayer.LeggiTutteLeClassi();
            // riempimento del ComboBox con le aule appena lette
            foreach (Classe a in listaClassiModello)
            {
                // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                cmbModelliClasse.Items.Add(a);
            }
            //cmbModelliClasse.SelectedIndex = 1;
            //cmbModelliClasse.SelectedItem = cmbClasseUtente.Items[1];

            // esempio: creazione di un nuovo banco con C#
            // i due banchi disegnati servono temporanemente perchè il 
            // gruppo che deve scrivere entro i banchi i nomi degli studenti
            // e dei computer possa fare le sue prove 
            Label GraficaBanco = new();
            GraficaBanco.BorderBrush = Brushes.Black;
            GraficaBanco.HorizontalAlignment = HorizontalAlignment.Left;
            GraficaBanco.VerticalAlignment = VerticalAlignment.Center;
            GraficaBanco.Background = Brushes.BurlyWood;
            GraficaBanco.FontWeight = FontWeights.Bold;
            TextBlock tb = new TextBlock();
            tb.TextAlignment = TextAlignment.Center;
            tb.Inlines.Add(new Run("PC1228"));
            tb.Inlines.Add(new LineBreak());
            tb.Inlines.Add(new Run("__________"));
            tb.Inlines.Add(new LineBreak());
            tb.Inlines.Add("Giorgio Salutini");
            GraficaBanco.Content = tb;
            AreaDisegno.Children.Add(GraficaBanco);
            Canvas.SetLeft(GraficaBanco, 250);
            Canvas.SetTop(GraficaBanco, 100);
        }
        private void MenuAula_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraAula();
        }
        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow wnd = new AboutWindow();
            wnd.Show();
        }
        private void btn_Banchi_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraBanchi();
        }
        private void ApriFinestraBanchi()
        {
            if (cmbModelliAule.SelectedItem == null)
            {
                MessageBox.Show("Selezionare un'aula fra i modelli", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BanchiWindow wnd = new BanchiWindow();
            wnd.Show();
        }
        private void btn_Aula_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraAula();
        }
        private void ApriFinestraAula()
        {
            if (cmbModelliAule.SelectedItem == null)
            {
                MessageBox.Show("Selezionare un'aula fra i modelli", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            AulaWindow wnd = new AulaWindow((Aula)cmbModelliAule.SelectedItem);
            wnd.Show();
        }
        private void btn_Salva_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void cmbModelliClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbModelliClasse.SelectedItem != null)
            {
                List<Studente> listaStudenti = BusinessLayer.LeggiStudentiClasse((Classe)cmbModelliClasse.SelectedItem);
                lstStudenti.ItemsSource = listaStudenti;
            }
            else
            {
                lstStudenti.ItemsSource = null;
            }
        }
        private void cmbModelliAule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbModelliAule.SelectedItem != null)
            {
                ((Aula)cmbModelliAule.SelectedItem).VisualizzaAulaEBanchi();
            }
        }
        private void chkStudenti_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void chkStudenti_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        private void chkComputer_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void chkComputer_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        private void btn_Computer_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraComputer();
        }
        private void ApriFinestraComputer()
        {
            if (cmbModelliAule.SelectedItem == null)
            {
                MessageBox.Show("Selezionare un'aula fra i modelli", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ComputerWindow wnd = new ComputerWindow((Aula)cmbModelliAule.SelectedItem);
            wnd.Show();
        }
        private void btn_Classe_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraClasse();
        }

        private void ApriFinestraClasse()
        {
            //if (cmbModelliAule.SelectedItem == null)
            //{
            //    MessageBox.Show("Selezionare un'aula fra i modelli", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            // se non c'è nulla di selezionato, la finestra aperta dovrà creare una nuova classe 
            ClasseWindow wnd = new ClasseWindow((Classe)cmbModelliClasse.SelectedItem);
            wnd.Show();
        }

        private void cmbcmbBanchiEStudenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO visualizzare l'aula con gli studenti 
        }
    }
}