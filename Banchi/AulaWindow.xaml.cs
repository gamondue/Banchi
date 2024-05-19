using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per Aula.xaml
    /// </summary>
    public partial class AulaWindow : Window
    {
        public AulaWindow(Aula selectedItem)
        {
            InitializeComponent();
            FinestrePorte.Visibility = Visibility.Hidden;
            Canvas.Visibility = Visibility.Hidden;           
        }
        private void btnClick_ConfermaDim(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtBase.Text) > 0.0 && Convert.ToDouble(txtAltezza.Text) > 0.0)
            {
                DimensioniAula.Visibility = Visibility.Hidden;
                FinestrePorte.Visibility = Visibility.Visible;
                Canvas.Visibility = Visibility.Visible;
                double base2 = Convert.ToDouble(txtBase.Text);
                double altezza2 = Convert.ToDouble(txtAltezza.Text);
                Aula aula1 = new("L13", altezza2, base2);              
            }
        }
    }
}
