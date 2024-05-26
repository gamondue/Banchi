using Banchi.Classi;
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
        internal string studenteSelezionato;
        internal Label labelSelezionata;

        List<Aula> listaAuleUtente;
        List<Classe> listaClassiUtente;

        List<Aula> listaAuleModello;
        List<Classe> listaClassiModello;
        List<Computer> listaComputer;

        public MainWindow()
        {
            InitializeComponent();
            BusinessLayer.Inizializzazioni();

            if (Utente.Accesso != Utente.RuoloUtente.ModificheAiModelli)
            {
                btn_Salva.Visibility = Visibility.Hidden;
            }
            // legge tutte le aule dal "database" e le mette in una lista
            listaAuleUtente = BusinessLayer.LeggiTutteLeAuleUtente();
            // riempimento del ComboBox con le aule appena lette
            if (listaAuleUtente != null)
                foreach (Aula a in listaAuleUtente)
                {
                    // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                    // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                    cmbAuleUtente.Items.Add(a);
                }

            listaClassiUtente = BusinessLayer.LeggiTutteLeClassiUtente();
            if (listaAuleUtente != null)
                foreach (Classe a in listaClassiUtente)
                {
                    cmbClasseUtente.Items.Add(a);
                }

            // riempio i combobox dei modelli, se l'utente ne ha il diritto
            listaAuleModello = BusinessLayer.LeggiTutteLeAule();
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
            listaClassiModello = BusinessLayer.LeggiTutteLeClassi();
            // riempimento del ComboBox con le aule appena lette
            foreach (Classe c in listaClassiModello)
            {
                // un combo può contenere oggetti di qualsiasi tipo, però per vederci qualcosa dentro
                // devo implementare il metodo ToString() dentro listaAuleUtente classe Aule
                cmbModelliClasse.Items.Add(c);
            }
            //cmbModelliClasse.SelectedIndex = 1;
            //cmbModelliClasse.SelectedItem = cmbClasseUtente.Items[1];

            listaComputer = BusinessLayer.LeggiTuttiIComputer();
            // riempimento del ComboBox con i computer appena letti
            foreach (Computer c in listaComputer)
            {
                lstComputer.Items.Add(c);
            }
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
            tb.Inlines.Add("Salutini Giorgio");
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
                MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BanchiWindow wnd = new BanchiWindow((Aula)cmbModelliAule.SelectedItem);
            wnd.Show();
        }
        private void btn_Aula_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraAula();
        }
        private void ApriFinestraAula()
        {
            //if (cmbModelliAule.SelectedItem == null)
            //{
            //    MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            AulaWindow wnd = new AulaWindow((Aula)cmbModelliAule.SelectedItem);
            wnd.Show();
        }
        private void btn_SalvataggioCondivisi_Click(object sender, RoutedEventArgs e)
        {
            BusinessLayer.ScriviTutteLeAule(listaAuleModello);
        }
        private void cmbModelliClasse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
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
                chkStudenti.IsChecked = true;
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
            if (this.IsLoaded)
                lstStudenti.Visibility = Visibility.Visible;
        }
        private void chkStudenti_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstStudenti.Visibility = Visibility.Collapsed;
        }
        private void chkComputer_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstComputer.Visibility = Visibility.Visible;
        }
        private void chkComputer_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                lstComputer.Visibility = Visibility.Collapsed;
        }
        private void btn_Computer_Click(object sender, RoutedEventArgs e)
        {
            ApriFinestraComputer();
        }
        private void ApriFinestraComputer()
        {
            //if (cmbModelliAule.SelectedItem == null)
            //{
            //    MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            ComputerWindow wnd = new ComputerWindow((Aula)cmbModelliAule.SelectedItem, (Computer)lstComputer.SelectedItem);
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
            //    MessageBox.Show("Selezionare un'aula fra i dati condivisi", "Errore",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            // se non c'è nulla di selezionato, la finestra aperta dovrà creare una nuova classe 
            ClasseWindow wnd = new ClasseWindow((Classe)cmbModelliClasse.SelectedItem);
            wnd.Show();
        }
        private void btn_AssociaStudente_Click(object sender, RoutedEventArgs e)
        {
            if (lstStudenti.SelectedItem != null)
            {
                studenteSelezionato = lstStudenti.SelectedItem.ToString();
                TextBlock tb = new TextBlock();
                tb.TextAlignment = TextAlignment.Center;
                tb.Inlines.Add(new Run("PC1228"));
                tb.Inlines.Add(new LineBreak());
                tb.Inlines.Add(new Run("__________"));
                tb.Inlines.Add(new LineBreak());
                tb.Inlines.Add(studenteSelezionato);
                labelSelezionata.Content = tb;
                labelSelezionata.BorderBrush = Brushes.LightCoral;
            }
        }
        private void click_Label(object sender, RoutedEventArgs e)
        {
            labelSelezionata = (Label)sender;
            labelSelezionata.BorderBrush = Brushes.Black;
        }
        private void cmbBanchiEStudenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO visualizzare l'aula con gli studenti 
        }
        private void btn_SalvataggioUtente_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_AssociaComputer_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_DistribuisciStudenti_Click(object sender, RoutedEventArgs e)
        {

        }
        private void txtFiltroComputer_TextChanged(object sender, TextChangedEventArgs e)
        {
            // filtraggio dei computer 
            string filterText = txtFiltroComputer.Text.ToLower(); // Converti in minuscolo per confronto case-insensitive
            lstComputer.Items.Clear(); // Rimuovi tutti gli elementi dalla ListBox
            // metti nella ListBox solo gli elementi che corrispondono alla stringa di filtro 
            foreach (Computer item in listaComputer)
            {
                if (item.ToString().ToLower().Contains(filterText))
                {
                    lstComputer.Items.Add(item); // Aggiungi solo gli elementi che corrispondono al filtro
                }
            }
        }
    }
}