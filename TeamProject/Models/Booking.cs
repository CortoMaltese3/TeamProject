using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Net;

namespace TeamProject.Models
{

    public class Booking
    {
        public int Id { get; set; }

        public int CourtId { get; set; }

        public int UserId { get; set; }

        [DisplayName("Booked Date/Time")]
        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy HH:mm}")]
        public DateTime BookedAt { get; set; }

        public int Duration { get; set; }

        public string BookKey { get; set; }

        public Court Court { get; set; }

        public User User { get; set; }

        private string qrCodeImageAsBase64;
        public string QrCodeImageAsBase64(Uri url)
        {
            string domain = url.Scheme + System.Uri.SchemeDelimiter + url.Host + (url.IsDefaultPort ? "" : ":" + url.Port);
            
            if (qrCodeImageAsBase64 == null)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"{domain}/Courts/Confirmed/{BookKey}", QRCodeGenerator.ECCLevel.Q);
                var imgType = Base64QRCode.ImageType.Jpeg;
                Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                qrCodeImageAsBase64 = qrCode.GetGraphic(20, Color.Black, Color.White, true, imgType);
            }

            return qrCodeImageAsBase64;
        }
    }
}
