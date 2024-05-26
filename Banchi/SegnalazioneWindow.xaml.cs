using MimeKit;
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
            //string oraScelta = testoOrario.Text, classeScelta = testoClasse.Text, bancoScelto = testoNomePC.Text;

            //MailAddress to = new MailAddress("gabriele.monti@ispascalcomandini.it");
            //MailAddress from = new MailAddress("sender.gamon@gmail.com");

            //MailMessage email = new MailMessage(from, to);
            //email.Subject = "Segnalazione di un Banco";
            //email.Body = oraScelta + '\t' + classeScelta + '\t' + bancoScelto;

            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 25;
            //smtp.Credentials = new NetworkCredential("sender.gamon@gmail.com", "ciaoGamon");
            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtp.EnableSsl = true;

            //try
            //{
            //    testoOrario.Text = "ciao";
            //    smtp.Send(email);
            //}
            //catch (SmtpException ex)
            //{
            //    Console.WriteLine(ex.ToString());
            //}


            //var client = new SmtpClient("smtp.gmail.com", 7070)
            //{
            //    Credentials = new NetworkCredential("sender.gamon@gmail.com", "ciaoGamon"),
            //    EnableSsl = true
            //};
            //client.Send("gamonsender.gamon@gmail.com", "gabriele.monti@ispascalcomandini.it",
            //    "Segnalazione problema su un computer",
            //    oraScelta + '\t' + classeScelta + '\t' + bancoScelto + '\t' +
            //    "Segnalazione di un problema su un computer");

            // uso di MailKit (da installare con NuGet)
            // vedi:  https://mailtrap.io/blog/csharp-send-email-gmail/
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Banchi, agente email", "sender.gamon@gmail.com"));
            email.To.Add(new MailboxAddress("Prof.Gabriele Monti", "gabriele.monti@ispascalcomandini.it"));
            email.Subject = "Segnalazione di un problema su un computer";
            // creazione di un messaggio di solo testo 
            email.Body = new TextPart("plain")
            {
                Text = "Ora della segnalazione: " + testoOrario.Text + "\n" +
                "Computer: " + testoNomePC.Text + "\n" +
                "Problema: " + testo.Text
            };
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);

                // la prossima dà errore di InvalidSecondFactor 
                client.Authenticate("sender.gamon@gmail.com", "ciaoGamon");

                //Enabled two-factor authentication
                //Having two - factor authentication enabled for a Gmail account is a common practice nowadays,
                //but it does create an obstacle when wanting to connect the account to a C# application. 
                //To go past this obstacle, you will need to create an “App Password” for the application by going
                //to the security settings of your Google account and then to “Signing in to Google” -> “App Passwords” -> “C#” -> “Other” -> “Generate”.
                //This password will need to be used in your C# code through hardcoding, which does pose a security risk, so do bear this in mind.

                client.Send(email);
                client.Disconnect(true);
            }
        }
    }
}
