using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EASendMail;
namespace cinema
{
    internal class SendMail
    {
        List<string> piletid;
        List<int> seat, row;
        decimal kokkuhind;
        string nimetus_hall, time;
        public SendMail(List<string> piletid, decimal kokkuhind, string nimetus_hall, List<int> seat, List<int> row, string time) 
        {
            this.time = time;
            this.row = row;
            this.seat = seat;
            this.piletid = piletid;
            this.kokkuhind = kokkuhind;
            this.nimetus_hall = nimetus_hall;
            Sendmail();
        }
        public void Sendmail() 
        {
            SmtpMail oMail = new SmtpMail("TryIt");

            // Your email address
            oMail.From = "cinemaTTHK@hotmail.com";

            // Set recipient email address
            oMail.To = "lastovskim@gmail.com";

            // Set email subject
            oMail.Subject = "Piletid";

            // Set email body
            string text = "";
            for (int i = 0; i < piletid.Count; i++)
            {
                text += piletid[i];
                text += "Rida -> " + row[i] + " Koht -> "+ seat[i] + "\n";
            }
            text += "Kokku -> " + kokkuhind.ToString() + "€" + "\nSaal -> " + nimetus_hall + " \nSeansi aeg -> " + time;
            oMail.TextBody = text;
            // Hotmail/Outlook SMTP server address
            SmtpServer oServer = new SmtpServer("smtp.office365.com");

            // If your account is office 365, please change to Office 365 SMTP server
            // SmtpServer oServer = new SmtpServer("smtp.office365.com");

            // User authentication should use your
            // email address as the user name.
            oServer.User = "cinemaTTHK@hotmail.com";

            // If you got authentication error, try to create an app password instead of your user password.
            // https://support.microsoft.com/en-us/account-billing/using-app-passwords-with-apps-that-don-t-support-two-step-verification-5896ed9b-4263-e681-128a-a6f2979a7944
            oServer.Password = "cinemaTARpv21";

            // use 587 TLS port
            oServer.Port = 587;

            // detect SSL/TLS connection automatically
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;


            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
            oSmtp.SendMail(oServer, oMail);
        }
    }
}
