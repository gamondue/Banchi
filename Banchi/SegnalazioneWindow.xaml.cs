using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Logica di interazione per SegnalazioneWindow.xaml
    /// </summary>
    public partial class SegnalazioneWindow : Window
    {
        public SegnalazioneWindow()
        {
            InitializeComponent();
            DateTime momentoProblema = DateTime.Now;
            string output = "";
            output += momentoProblema.Hour + ":" + momentoProblema.Minute + " " + momentoProblema.Day + "/" + momentoProblema.Month + "/" + momentoProblema.Year;
            testoOrario.Text = output;
        }

        private void salva_click(object sender, RoutedEventArgs e)
        {
            string oraScelta = testoOrario.Text, classeScelta = testoClasse.Text, bancoScelto = testoNomePC.Text;

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


            var client = new SmtpClient("smtp.gmail.com", 7070)
            {
                Credentials = new NetworkCredential("sender.gamon@gmail.com", "ciaoGamon"),
                EnableSsl = true
            };
            client.Send("myusername@gmail.com", "gabriele.monti@ispascalcomandini.it", "Segnalazione di un Banco", oraScelta + '\t' + classeScelta + '\t' + bancoScelto);
        }
    }
}
