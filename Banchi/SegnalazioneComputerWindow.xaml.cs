using System.Windows;

namespace Banchi
{
    /// <summary>
    /// Logica di interazione per SegnalazioneWindow.xaml
    /// </summary>
    public partial class SegnalazioneWindow : Window
    {
        Computer computer;
        public SegnalazioneWindow(Computer computer)
        {
            InitializeComponent();

            this.computer = computer;
            DateTime momentoProblema = DateTime.Now;
            string output = "";
            output += momentoProblema.Hour + ":" + momentoProblema.Minute + " " + momentoProblema.Day + "/" + momentoProblema.Month + "/" + momentoProblema.Year;
            testoOrario.Text = output;
        }

        private void salva_click(object sender, RoutedEventArgs e)
        {
            string oraScelta = testoOrario.Text, classeScelta = testoClasse.Text, bancoScelto = testoNomePC.Text;

            //string oggetto = "Segnalazione di un problema che riguarda computer XXXXX";
            //string testo = "Ora della segnalazione: " + testoOrario.Text + "\n" +
            //    "Computer: " + testoNomePC.Text + "\n" +
            //    "Problema: " + testo.Text;
            //// ""oraScelta + '\t' + classeScelta + '\t' + bancoScelto""

            Banchi.BusinessLayer.SpeditoreEmail("gamon@ingmonti.it", "Prova Oggetto",
                "Corpo del messaggio solo testuale che sto provando a mandare.");
            BusinessLayer.SpeditoreEmail.EmailMailkit("gamon@ingmonti.it", "Prova Oggetto",
                "Corpo del messaggio solo testuale che sto provando a mandare.");
            BusinessLayer.SpeditoreEmail.EmailSpedizioneDiretta("gamon#ingmonti.it", "Prova Oggetto",
                "Corpo del messaggio solo testuale che sto provando a mandare.");


        }
    }
}
