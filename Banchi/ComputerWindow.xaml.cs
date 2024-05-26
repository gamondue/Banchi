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

        public ComputerWindow(Aula aula, Computer computer = null)
        {
            InitializeComponent();
            BanchiComboBox.Items.Add(computer);

            this.computer = computer;
        }

        private void SegnalazioneWindow_click(object sender, RoutedEventArgs e)
        {
            SegnalazioneWindow wnd = new SegnalazioneWindow(computer);
            wnd.Show();
        }
        private void ModificaComputer_Click(object sender, RoutedEventArgs e)
        {
            IPMod.IsReadOnly = false;
            NomeDispositivoMod.IsReadOnly = false;
            MarcaMod.IsReadOnly = false;
            NotaComputer.IsReadOnly = false;
            ProcessoreMod.IsReadOnly = false;
            TipoSistemaMod.IsReadOnly = false;
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
    }
}
