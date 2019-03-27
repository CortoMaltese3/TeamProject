using System;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;
using TeamProject.Models;

namespace TeamProject
{
    public class SmtpMessageChunk
    {
        public static void SendMessageSmtp( Booking booking, Branch branch)
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Team Project crew", "TeamProject@sandbox5e629bb48a664a34bd30b011a90f91e1.mailgun.org"));
            mail.To.Add(new MailboxAddress(booking.User.UserName, booking.User.Email));
            mail.Subject = "Confirmation Email for Court Booking";
            mail.Body = new TextPart("html")
            {
                Text = $@"<h2> {booking.User.UserName} Thanks for booking.</h2>
                    <br />
                    <div> You have book <strong> {booking.Court.Name} </strong> at <strong>{booking.BookedAt}</strong></div>
                    <br />
                    <span>Your booking number is <strong>{booking.BookKey}</strong></span>
                    <br />
                    <br />
                    <span>You can find the Court in {branch.City} at {branch.Address}</span>
                    <br />
                    <span><strong>Price:</strong> {booking.Court.Price}&euro;",
            };

            // Send it!
            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailgun.org", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Properties.Settings.Default.Username,Properties.Settings.Default.Password);

                client.Send(mail);
                client.Disconnect(true);
            }
        }

    }
}
