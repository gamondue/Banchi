using System.Windows;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {
        
        
        public ComputerWindow(Aula aula, Banco banco = null)
        {
            InitializeComponent();
            BanchiComboBox.Items.Add(banco);
        }

        private void SegnalazioneWindow_click(object sender, RoutedEventArgs e)
        {
            SegnalazioneWindow wnd = new SegnalazioneWindow();
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
