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
        }

        private void SegnalazioneWindow_click(object sender, RoutedEventArgs e)
        {
            SegnalazioneWindow wnd = new SegnalazioneWindow();
            wnd.Show();
        }
    }
}
