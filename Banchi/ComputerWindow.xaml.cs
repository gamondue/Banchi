using System.Windows;
using static Banchi.Computer;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {
        //Computer computerNuovo;
        MessageBoxButton bottone = MessageBoxButton.YesNo;
        MessageBoxResult result;
        private Computer computerCorrente;
        private List<Computer>? tuttiIComputer;
        private List<Computer> listaCorrenteComputer = new();

        public ComputerWindow(Aula aula, Computer computer = null)
        {
            InitializeComponent();

            if (computer != null)
            {
                computerCorrente = computer;
                txtIndirizzoIp.IsReadOnly = false;
                txtNomeDispositivo.IsReadOnly = false;
                txtMarca.IsReadOnly = false;
                txtNoteComputer.IsReadOnly = false;
                txtProcessore.IsReadOnly = false;
                txtSistema.IsReadOnly = false;

                FromObjectToUi(computerCorrente);
            }
            tuttiIComputer = BusinessLayer.LeggiTuttiIComputer();
            ComputerGrid.ItemsSource = BusinessLayer.LeggiTuttiIComputer();

            listaCorrenteComputer = (List<Computer>)BusinessLayer.LeggiTuttiIComputer();

            cmbStatoComputer.ItemsSource =
                Enum.GetValues(typeof(Computer.cmbStatoComputer)).Cast<Computer.cmbStatoComputer>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (computerCorrente != null && computerCorrente.NomeDispositivo != null)
            {
                txtFiltroComputer.Text = computerCorrente.NomeDispositivo.Substring(0, 4);
            }
        }
        private void FromObjectToUi(Computer computer)
        {
            txtNomeDispositivo.Text = computer.NomeDispositivo;
            txtMarca.Text = computer.MarcaComputer;
            txtModello.Text = computer.Modello;
            txtProcessore.Text = computer.Processore;
            txtSistema.Text = computer.TipoSistema;
            txtIndirizzoIp.Text = computer.IndirizzoIPComputer;
            txtNoteComputer.Text = computer.NoteComputer;
            cmbStatoComputer.SelectedValue = computer.Stato;
        }
        private Computer FromUiToComputer()
        {
            Computer computer = new(txtNomeDispositivo.Text);
            computer.MarcaComputer = txtMarca.Text;
            computer.Modello = txtModello.Text;
            computer.Processore = txtProcessore.Text;
            computer.TipoSistema = txtSistema.Text;
            computer.IndirizzoIPComputer = txtIndirizzoIp.Text;
            computer.NoteComputer = txtNoteComputer.Text;
            computer.Stato = (cmbStatoComputer)cmbStatoComputer.SelectedItem;
            return computer;
        }
        private void SegnalazioneWindow_click(object sender, RoutedEventArgs e)
        {
            SegnalazioneWindow wnd = new SegnalazioneWindow(computerCorrente);
            if (result == MessageBoxResult.Yes)
            {
                wnd.Show();
            }
        }
        private void ModificaComputer_Click(object sender, RoutedEventArgs e)
        {
            txtIndirizzoIp.IsReadOnly = false;
            txtNomeDispositivo.IsReadOnly = false;
            txtMarca.IsReadOnly = false;
            txtProcessore.IsReadOnly = false;
            txtProcessore.IsReadOnly = false;
            txtSistema.IsReadOnly = false;
        }
        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void Nota_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void SalvaButton_Click(object sender, RoutedEventArgs e)
        {
            Computer computerNuovo = FromUiToComputer();
            BusinessLayer.SalvaComputer(computerNuovo, computerCorrente.NomeDispositivo);
            ComputerGrid.ItemsSource = BusinessLayer.LeggiTuttiIComputer();
        }
        private void GestisciLab_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 600;
        }
        private void GeneraComputer_Click(object sender, RoutedEventArgs e)
        {
            lstComputerLab.Items.Clear();
            Computer tempComputer = FromUiToComputer();
            List<Computer> listaNuoviAula = BusinessLayer.GeneraComputers(txtSchemaNome.Text, tempComputer,
                Convert.ToInt32(txtNumeroComputerInizio.Text), Convert.ToInt32(txtNumeroComputerFine.Text));
            lstComputerLab.ItemsSource = listaNuoviAula;
            BusinessLayer.AggiungiComputers(listaNuoviAula);
        }
        private void ComputerGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComputerGrid.SelectedItem == null)
            {
                return;
            }
            computerCorrente = (Computer)ComputerGrid.SelectedItem;
            FromObjectToUi(computerCorrente);
            //DataContext = null;
            //DataContext = computerCorrente;
        }
        private void RimuoviComputer_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomeDispositivo.Text == null || txtNomeDispositivo.Text == "")
            {
                MessageBox.Show("Per eliminare un computer, scriverne il nome nella casella 'Nome'");
                return;
            }
            BusinessLayer.EliminaComputer(txtNomeDispositivo.Text);
            ComputerGrid.ItemsSource = BusinessLayer.ComputerFiltrati(txtFiltroComputer.Text, listaCorrenteComputer);
        }
        private void AggiungiComputer_Click(object sender, RoutedEventArgs e)
        {
            if (BusinessLayer.CercaComputer(txtNomeDispositivo.Text,
                listaCorrenteComputer) != null)
            {
                MessageBox.Show("Per aggiungere un computer, dare un nome diverso da quelli esistenti");
                return;
            }
            Computer nuovo = FromUiToComputer();
            BusinessLayer.SalvaComputer(nuovo, nuovo.NomeDispositivo);
            ComputerGrid.ItemsSource = BusinessLayer.ComputerFiltrati(txtFiltroComputer.Text, listaCorrenteComputer);
        }
        private void lstComputer_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        private void txtSchemaNome_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int min, max;
            // composizione dell'elenco dei nomi dei computerNuovo che si vogliono creare
            //List<string> nomiComputer = new();
            lstComputerLab.ItemsSource = null;
            lstComputerLab.Items.Clear();
            if (!int.TryParse(txtNumeroComputerInizio.Text, out min) ||
                !int.TryParse(txtNumeroComputerFine.Text, out max))
            {
                Console.Beep();
                //MessageBox.Show(@"Inserire un numero valido in ""A"" o ""Da""");
                return;
            }
            for (int i = min; i <= max; i++)
            {
                lstComputerLab.Items.Add(txtSchemaNome.Text.Replace("*", i.ToString("00")));
            }
        }
        private void txtFiltroComputer_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ComputerGrid.ItemsSource = BusinessLayer.ComputerFiltrati(txtFiltroComputer.Text, listaCorrenteComputer);
        }
    }
}
