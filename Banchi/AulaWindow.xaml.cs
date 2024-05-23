using System.Windows;
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
        private Point startPosition;

        public AulaWindow(Aula selectedItem)
        {
            InitializeComponent();
            txtAltezza.Text = "2000";
            txtBase.Text = "1000";
            a1 = new Aula("L12", 2000, 1000, grafica);
            canvasC.Children.Add(grafica); //andrebbe passata la classe a1 e non la label grafica
            timer.Interval = new TimeSpan(1);
            timer.Start();
            timer.Tick += Timer_Tick;

            FinestrePorte.Visibility = Visibility.Hidden;
            canvasC.Visibility = Visibility.Visible;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            //CONTROLLARE SE NUMERI TROPPO GRANDI O PICCOLI
            double altezza2 = Convert.ToDouble(txtAltezza.Text);
            double base2 = Convert.ToDouble(txtBase.Text);
            //a1.AltezzaInCentimetri = altezza2;
            //a1.BaseInCentimetri = base2;
            //a1.NomeAula = txtNome.Text;

            grafica.Width = base2;
            grafica.Height = altezza2;
            // da controllare
            // bisogna modificare la classe
            Canvas.Visibility = Visibility.Hidden;
        }
        private void btnClick_ConfermaDim(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtBase.Text) > 0.0 && Convert.ToDouble(txtAltezza.Text) > 0.0)
            {
                DimensioniAula.Visibility = Visibility.Hidden;
                FinestrePorte.Visibility = Visibility.Visible;
                canvasC.Visibility = Visibility.Visible;
                double base2 = Convert.ToDouble(txtBase.Text);
                double altezza2 = Convert.ToDouble(txtAltezza.Text);
                a1 = new Aula(txtNome.Text, altezza2, base2, grafica);
            }
        }

        private void btn_ConfermaFinestra_Click(object sender, RoutedEventArgs e)
        {
            double base2 = Convert.ToDouble(txtBase.Text);
            double altezza2 = Convert.ToDouble(txtAltezza.Text);
            Aula aula1 = new("L13", altezza2, base2, grafica);
        }
    }
}
