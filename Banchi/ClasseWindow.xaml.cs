using System.Windows;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per ClasseWindow.xaml
    /// </summary>
    public partial class ClasseWindow : Window
    {
        Classe classe;
        public ClasseWindow(Classe classe)
        {
            InitializeComponent();

            if (classe != null)
            {
                this.classe = classe;
            }
            else
            {

            }
        }
    }
}
