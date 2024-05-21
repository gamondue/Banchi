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
using System.Windows.Threading;
using Label = System.Windows.Controls.Label;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per Aula.xaml
    /// </summary>
    public partial class AulaWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        Label grafica = new Label();
        Aula a1;
        bool isDragging = false;
        private Point startPosition;

        public AulaWindow()
        {
            InitializeComponent();
            a1 = new Aula("L12", 200, 100);
            canvasC.Children.Add(grafica);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(1);
            timer.Start();
            timer.Tick += Timer_Tick;
            // canvasC.Children.Add(grafica); spostato sopra, non dovrebbe cambiare nulla

            FinestrePorte.Visibility = Visibility.Hidden;
            canvasC.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            double altezza2 = Convert.ToDouble(txtAltezza.Text);
            double base2 = Convert.ToDouble(txtBase.Text);
            a1.AltezzaInMetri = altezza2;
            a1.BaseInMetri = base2;
            a1.NomeAula = txtNome.Text;
        }

        private void btnClick_Conferma(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtBase.Text) > 0.0 && Convert.ToDouble(txtAltezza.Text) > 0.0)
            {
                DimensioniAula.Visibility = Visibility.Hidden;
                FinestrePorte.Visibility = Visibility.Visible;
                canvasC.Visibility = Visibility.Visible;
                double base2 = Convert.ToDouble(txtBase.Text);
                double altezza2 = Convert.ToDouble(txtAltezza.Text);
                Aula aula1 = new(txtNome.Text, altezza2, base2);
            }
        }

        private void btn_ConfermaFinestra_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
