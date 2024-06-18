using System.Windows;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {
        // TODO mettere un generatore automatico di dati dei computer: 
        // fare due text box che contengano due numeri: "da" "a" di computer da generare automaticamente 
        // fare un bottone "genera computer aula", che crea il numero di computer indicato nella textbox
        // i nuovi computer vengono generati con i valori attuali che sono scritti nelle textbox dei dati 
        // il nome deve essere formato come:
        // PC<numero di due cifre che corrisponde all'aula><numero progressivo da 1 al numero di CP dell'aula>
        // per questo è necessario aggiungere una textbox con il nome dell'aula
        // ???? i nuovi computer generati li mettiamo in una listbox qui ???? 
        Computer computer;
        MessageBoxButton bottone = MessageBoxButton.YesNo;
        MessageBoxResult result;
        private List<Computer>? tuttiIComputer;
        private Computer currentComputer;

        public ComputerWindow(Aula aula, Computer computer = null)
        {
            InitializeComponent();

            currentComputer = computer;

            if (computer != null)
            {
                IPMod.IsReadOnly = false;
                NomeDispositivoMod.IsReadOnly = false;
                MarcaMod.IsReadOnly = false;
                NoteComputer.IsReadOnly = false;
                ProcessoreMod.IsReadOnly = false;
                TipoSistemaMod.IsReadOnly = false;

                FromObjectToUi(computer);
            }
            tuttiIComputer = BusinessLayer.LeggiTuttiIComputer();
            ComputerGrid.ItemsSource = BusinessLayer.LeggiTuttiIComputer();

            this.computer = computer;
        }
        private void FromObjectToUi(Computer computer)
        {
            NomeDispositivoMod.Text = computer.NomeDispositivo;
            MarcaMod.Text = computer.MarcaComputer;
            ProcessoreMod.Text = computer.Processore;
            TipoSistemaMod.Text = computer.TipoSistema;
            IPMod.Text = computer.IndirizzoIPComputer;
            NoteComputer.Text = computer.NoteComputer;
            StatoComputer.SelectedValue = (string)computer.Stato;
        }
        private Computer FromUiToComputer()
        {
            Computer computer = new(NomeDispositivoMod.Text);
            computer.MarcaComputer = MarcaMod.Text;
            computer.Processore = ProcessoreMod.Text;
            computer.TipoSistema = TipoSistemaMod.Text;
            computer.IndirizzoIPComputer = IPMod.Text;
            computer.NoteComputer = NoteComputer.Text;
            computer.Stato = (string)StatoComputer.SelectedValue;
            return computer;
        }
        private void SegnalazioneWindow_click(object sender, RoutedEventArgs e)
        {
            SegnalazioneWindow wnd = new SegnalazioneWindow(computer);
            if (result == MessageBoxResult.Yes)
            {
                wnd.Show();
            }
        }
        private void ModificaComputer_Click(object sender, RoutedEventArgs e)
        {
            IPMod.IsReadOnly = false;
            NomeDispositivoMod.IsReadOnly = false;
            MarcaMod.IsReadOnly = false;
            ProcessoreMod.IsReadOnly = false;
            ProcessoreMod.IsReadOnly = false;
            TipoSistemaMod.IsReadOnly = false;
            StatoComputer.SelectedValue = "In uso";
        }
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void nota_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void SalvaButton_Click(object sender, RoutedEventArgs e)
        {
            BusinessLayer.SalvaComputer();
        }
        private void GestisciLab_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 600;

        }
        private void GeneraComputer_Click(object sender, RoutedEventArgs e)
        {
            Computer tempComputer = FromUiToComputer();

            List<Computer> listaNuoviAula = BusinessLayer.GeneraComputer(txtSchemaNome.Text, tempComputer,
                Convert.ToInt32(txtNumeroComputerInizio.Text), Convert.ToInt32(txtNumeroComputerFine.Text));
            lstComputerLab.ItemsSource = listaNuoviAula;
        }
        private void ComputerGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            currentComputer = (Computer)ComputerGrid.SelectedItem;
            FromObjectToUi(currentComputer);
            //DataContext = null;
            //DataContext = currentComputer;
        }
        private void RimuoviComputer_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AggiungiComputer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
