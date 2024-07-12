using Banchi.Classi;
using System.IO;
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

            txtMomentoProblema.Text = momentoProblema.ToString("dd-MM-yyyy HH");
            txtAutoreSegnalazione.Text = Utente.Username;

            txtNomeComputer.Text = computer.Nome;
            txtMarca.Text = computer.Marca;
            txtModello.Text = computer.Modello;
            txtProcessore.Text = computer.Processore;
            txtSistemaOperativo.Text = computer.SistemaOperativo;
            txtIndirizzoIp.Text = computer.IndirizzoIP;
            txtStatoComputer.Text = computer.Stato.ToString();
        }
        private void Spedisci_Click(object sender, RoutedEventArgs e)
        {
            string oggetto = "";
            if ( (string)(cmbUrgenzaSegnalazione.SelectedValue) == "Sabotaggio")
                oggetto = "Sabotaggio al computer: " + txtNomeComputer.Text + " segnalato da: " + txtAutoreSegnalazione.Text;
            else 
                oggetto = "Segnalazione di un problema sul computer: " + txtNomeComputer.Text + " da: " + txtAutoreSegnalazione.Text;
            string testoMessaggio = "Data e ora della segnalazione: " + txtMomentoProblema.Text + "\n" +
                "Urgenza: " + cmbUrgenzaSegnalazione.Text + "\n" +
                "Computer: " + txtNomeComputer.Text + "\t" + "Marca: " + txtMarca.Text + "\t" + "Modello: " + txtModello.Text + "\n" +
                "Processore: " + txtProcessore.Text + "\t" + "Sistema Operativo: " + txtSistemaOperativo.Text + "\n" +
                "Indirizzo IP: " + txtIndirizzoIp.Text + "\t" + "Stato: " + txtStatoComputer.Text + "\n\n" +
                "Problema: \n" + txtTestoSegnalazione.Text;

            //EmailOut.SpedizioneDiretta("gamon@ingmonti.it", "Prova Oggetto",
            //    "Corpo del messaggio solo testuale che sto provando a mandare.");
            //EmailOut.MailKit("gamon@ingmonti.it", "Prova Oggetto",
            //    "Corpo del messaggio solo testuale che sto provando a mandare.");
            
            EmailOut.LancioDelMailerDiDefault("gamon@ingmonti.it", oggetto, testoMessaggio);

            string messaggioCompleto = "Messaggio spedito in data: " + DateTime.Now + "\nOggetto : " 
                + oggetto + "\n" + "Messaggio: " + testoMessaggio;
            BusinessLayer.SalvaSegnalazione(messaggioCompleto);

            MessageBox.Show("Programma mailer lanciato, completare la spedizione dell'email", 
                "Invio Segnalazione", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }
    }
}
