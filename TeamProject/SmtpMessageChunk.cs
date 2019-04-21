using System;
using System.Collections.Generic;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;
using TeamProject.Models;
using TeamProject.ModelsViews;

namespace TeamProject
{
    public class SmtpMessageChunk
    {
        public static void SendMessageSmtp(Booking booking, Branch branch, Uri url)
        {
            // Compose a message
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Kickit crew", "TeamProject@sandbox5e629bb48a664a34bd30b011a90f91e1.mailgun.org"));
            mail.To.Add(new MailboxAddress(booking.User.UserName, booking.User.Email));
            mail.Subject = "Confirmation Email for Court Booking";
            mail.Body = new TextPart("html")
            {
                Text = $@"<h2> {booking.User.UserName}, thanks for booking.</h2>
                    <br />
                    <div> You have booked <strong> {booking.Court.Name} </strong> at <strong>{booking.BookedAt}</strong></div>
                    <br />
                    <span>Your booking number is <strong>{booking.BookKey}</strong></span>
                    <div>
                        <img style='height:256px' src='data:image/jpeg;base64,{booking.QrCodeImageAsBase64(url)}'>
                    </div>
                    <br />
                    <br />
                    <span>You can find the Court at {branch.Address}</span>
                    <br />
                    <span><strong>Price:</strong> {booking.Court.Price}&euro;",
            };

            // Send it!
            SendEmail(mail);
        }

        public static void SendContactFormEmail(ContactForm contactForm)
        {
            MimeMessage mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(contactForm.FullName, "TeamProject@sandbox5e629bb48a664a34bd30b011a90f91e1.mailgun.org"));
            mail.To.Add(new MailboxAddress("George Lymperopoulos", "lympe7@hotmail.com"));
            mail.Subject = contactForm.SubjectSelector.ToString();
            mail.Body = new TextPart("html")
            {
                Text = $@"<h3>CONTACT FORM</h3>
                    <br />
                    <div><strong>User: </strong>{contactForm.FullName}</div>
                    <br />
                    <span><strong>Subject: </strong>{contactForm.SubjectSelector}</span>                    
                    <br />
                    <br />
                    <span><strong>Message: </strong>{contactForm.Body}</span>
                    <br />
                    <span><strong>Email : </strong>{contactForm.Email}"
            };

            // Send it!
            SendEmail(mail);


        }

        private static void SendEmail(MimeMessage mail)
        {

            using (var client = new SmtpClient())
            {
                // XXX - Should this be a little different?
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.mailgun.org", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Properties.Settings.Default.Username,
                    Properties.Settings.Default.Password);
                try
                {
                    client.Send(mail);
                }
                catch (Exception)
                {

                    //TODO catch Exception;
                }

                client.Disconnect(true);
            }
        }
    }
}
