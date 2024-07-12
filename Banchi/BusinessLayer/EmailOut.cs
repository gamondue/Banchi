using MimeKit;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace Banchi
{
    internal static class EmailOut
    {
        internal static void SpedizioneDiretta(string destinatarioEmail, string oggettoDellEmail, string testoEmail)
        {
            // !!!! TODO !!!! metodo da aggiustare perché non funziona 
            MailAddress to = new MailAddress(destinatarioEmail);
            MailAddress from = new MailAddress("sender.gamon@gmail.com");

            MailMessage email = new MailMessage(from, to);
            email.Subject = oggettoDellEmail;
            email.Body = testoEmail;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.Credentials = new NetworkCredential("sender.gamon@gmail.com", "ciaoGamon");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //var client = new SmtpClient("smtp.gmail.com", 7070)
            //{
            //    Credentials = new NetworkCredential("sender.gamon@gmail.com", "ciaoGamon"),
            //    EnableSsl = true
            //};
            //client.Send("gamonsender.gamon@gmail.com", "gabriele.monti@ispascalcomandini.it",
            //    "Segnalazione problema su un computer",
            //    oraScelta + '\t' + classeScelta + '\t' + bancoScelto + '\t' +
            //    "Segnalazione di un problema su un computer");
        }
        internal static void MailKit(string destinatarioEmail, string oggettoDellEmail, string testoEmail)
        {
            // !!!! TODO !!!! metodo da aggiustare perché non funziona 

            // uso di MailKit (da installare con NuGet)
            // vedi:  https://mailtrap.io/blog/csharp-send-email-gmail/
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Banchi, agente email", "sender.gamon@gmail.com"));
            email.To.Add(new MailboxAddress("Prof.Gabriele Monti", "gabriele.monti@ispascalcomandini.it"));
            email.Subject = "Segnalazione di un problema su un computer";
            // creazione di un messaggio di solo testo 
            email.Body = new TextPart("plain")
            {
                ////////Text = "Ora della segnalazione: " + testoOrario.Text + "\n" +
                ////////"Computer: " + testoNomePC.Text + "\n" +
                ////////"Problema: " + testo.Text
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
        internal static void LancioDelMailerDiDefault(string destinatarioEmail, string oggettoDellEmail, string testoEmail)
        {
            string recipient = destinatarioEmail;
            string subject = oggettoDellEmail;
            string body = testoEmail;

            string mailto = $"mailto:{recipient}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
            // lancio del mailer di default, passandogli i parametri per la nuova email
            try
            {
                Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
                Console.WriteLine("Il Mailer è partito!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore nell'apertura del mailer: " + ex.Message);
            }
        }
    }
}
