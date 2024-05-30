using System.Windows;
using System.Windows.Controls;
using Label = System.Windows.Controls.Label;


namespace Banchi
{
    /// <summary>
    /// Logica di interazione per Aula.xaml
    /// </summary>
    public partial class AulaWindow : Window
    {
        Label grafica = new Label();
        Aula a1;

        bool finestra = false;
        bool dimensione = true;
        private Point startPosition;
        private Point lato1;
        private Point lato2;
        private Point lato3;
        private Point lato4;

        public AulaWindow(Aula aula)
        {
            InitializeComponent();
            //METTERE FILE !!!TODOOOO!!!
            //valori predefiniti iniziali
            startPosition.X = 192;
            startPosition.Y = 92;
            // se l'aula passata è nulla, la creo nuova, altrimenti me la tengo 
            if (a1 == null)
            {
                a1 = new Aula("", 930, 1900, grafica);    //max lenghth = 1268,height = 614
                                                          //altezza*1,465
                                                          //base*1,34
            }
            else
            {
                this.a1 = aula;
            }

            canvasC.Children.Add(grafica);

            FinestrePorte.Visibility = Visibility.Hidden;
            canvasC.Visibility = Visibility.Visible;
        }
        private void btnClick_ConfermaDim(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(txtBase.Text) > 0.0 && Convert.ToDouble(txtAltezza.Text) > 0.0 && txtNome.Text != "")
            {
                //non si può più modificare la dimensione
                DimensioniAula.IsEnabled = false;
                FinestrePorte.Visibility = Visibility.Visible;

                Canvas.Visibility = Visibility.Visible;
                //prendo valori inseriti
                double base2 = Convert.ToDouble(txtBase.Text);
                double altezza2 = Convert.ToDouble(txtAltezza.Text);
                int grado = Convert.ToInt32(txtGrado.Text); // cos'è il grado? 
                string nomeAula = txtNome.Text;
                dimensione = false;
                Aula aula1 = new(nomeAula, altezza2, base2);
            }
        }
        private void btn_ConfermaFinestra_Click(object sender, RoutedEventArgs e)
        {
            if (radioBtnFinestre.IsChecked == true)
            {
                finestra = true;
            }
            else
            {
                finestra = false;
            }
        }
        private void btn_ConfermaElemento_Click(object sender, RoutedEventArgs e)
        {
            if (txtLato.Text != "1" && txtLato.Text != "2" && txtLato.Text != "3" && txtLato.Text != "4")
            {
                MessageBox.Show("Il numero del lato deve essere compreso tra 1 e 4");
            }
            if (txtLato.Text == "1" || txtLato.Text == "3")
            {
                if (Convert.ToDouble(txtDistanzaP.Text) < 0 || Convert.ToDouble(txtDistanzaP.Text) > Convert.ToDouble(txtAltezza.Text))
                {
                    MessageBox.Show("Distanza non accettabile");
                }
            }
            else
            {
                if (Convert.ToDouble(txtDistanzaP.Text) < 0 || Convert.ToDouble(txtDistanzaP.Text) > Convert.ToDouble(txtBase.Text))
                {
                    MessageBox.Show("Distanza non accettabile");
                }
            }
            if (txtLato.Text == "1" || txtLato.Text == "3")
            {
                if (Convert.ToDouble(txtDistanzaP.Text) < 1 || Convert.ToDouble(txtDistanzaP.Text) > Convert.ToDouble(txtAltezza.Text))
                {
                    MessageBox.Show("Larghezza non accettabile");
                }
            }
            else
            {
                if (Convert.ToDouble(txtDistanzaP.Text) < 1 || Convert.ToDouble(txtDistanzaP.Text) > Convert.ToDouble(txtBase.Text))
                {
                    MessageBox.Show("Larghezza non accettabile");
                }
            }


        }
        private void txtAltezza_TextChanged(object sender, TextChangedEventArgs e)
        {
            grafica.Height = Convert.ToDouble(txtAltezza.Text);
        }
        private void txtBase_TextChanged(object sender, TextChangedEventArgs e)
        {
            grafica.Width = Convert.ToDouble(txtBase.Text);
        }
        private void txtDistanzaP_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtLarghezza_TextChanged(object sender, TextChangedEventArgs e)
        {


        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //MessageBox.Show("akudgdp");
        }
    }
}