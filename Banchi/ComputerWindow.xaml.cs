using System.Windows;
using static Banchi.Computer;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {
        Computer computer;
        MessageBoxButton bottone = MessageBoxButton.YesNo;
        MessageBoxResult result;
        private List<Computer>? tuttiIComputer;
        private Computer currentComputer;
        private List<Computer> listaComputer = new();

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

            listaComputer = BusinessLayer.LeggiTuttiIComputer();
            this.computer = computer;

            StatoComputer.ItemsSource = 
                Enum.GetValues(typeof(Computer.StatoComputer)).Cast<Computer.StatoComputer>();
        }
        private void FromObjectToUi(Computer computer)
        {
            NomeDispositivoMod.Text = computer.NomeDispositivo;
            MarcaMod.Text = computer.MarcaComputer;
            ProcessoreMod.Text = computer.Processore;
            TipoSistemaMod.Text = computer.TipoSistema;
            IPMod.Text = computer.IndirizzoIPComputer;
            NoteComputer.Text = computer.NoteComputer;
            StatoComputer.SelectedValue = computer.Stato;
        }
        private Computer FromUiToComputer()
        {
            Computer computer = new(NomeDispositivoMod.Text);
            computer.MarcaComputer = MarcaMod.Text;
            computer.Processore = ProcessoreMod.Text;
            computer.TipoSistema = TipoSistemaMod.Text;
            computer.IndirizzoIPComputer = IPMod.Text;
            computer.NoteComputer = NoteComputer.Text;
            computer.Stato = (StatoComputer)StatoComputer.SelectedItem;
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
            Computer computer = FromUiToComputer(); 
            BusinessLayer.SalvaComputer(computer);
        }
        private void GestisciLab_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 600;
        }
        private void GeneraComputer_Click(object sender, RoutedEventArgs e)
        {
            lstComputerLab.Items.Clear(); 
            Computer tempComputer = FromUiToComputer();
            List<Computer> listaNuoviAula = BusinessLayer.GeneraComputer(txtSchemaNome.Text, tempComputer,
                Convert.ToInt32(txtNumeroComputerInizio.Text), Convert.ToInt32(txtNumeroComputerFine.Text));
            lstComputerLab.ItemsSource = listaNuoviAula;
            BusinessLayer.AggiungiComputers(listaNuoviAula);
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
            if (BusinessLayer.CercaComputer(NomeDispositivoMod.Text,
                listaComputer) != null)
            {
                MessageBox.Show("Per aggiungere un computer, dare un nome diverso da quelli esistenti");
                return; 
            }
            Computer nuovo = FromUiToComputer();
            BusinessLayer.SalvaComputer(nuovo); 
        }
        private void lstComputer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void txtSchemaNome_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int min, max;
            // composizione dell'elenco dei nomi dei computer che si vogliono creare
            //List<string> nomiComputer = new();
            lstComputerLab.ItemsSource= null; 
            lstComputerLab.Items.Clear();
            if (!int.TryParse(txtNumeroComputerInizio.Text, out min) || 
                !int.TryParse(txtNumeroComputerFine.Text, out max))
            {
                MessageBox.Show(@"Inserire un numero valido in ""A"" o ""Da""");
                return;
            }
            for (int i = min; i <= max; i++)
            {
                lstComputerLab.Items.Add(txtSchemaNome.Text.Replace("*", i.ToString("00")));
            }
        }
    }
}
