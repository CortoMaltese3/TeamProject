using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Net;
using TeamProject.Dal;

namespace TeamProject.Models
{

    public class Booking
    {
        public int Id { get; set; }

        [TableField]
        public int CourtId { get; set; }

        [TableField]
        public int UserId { get; set; }

        [TableField]
        [DisplayName("Booked Date/Time")]
        [DisplayFormat(DataFormatString = "{0:dddd dd/MM/yyyy HH:mm}")]
        public DateTime BookedAt { get; set; }

        [TableField]
        public int Duration { get; set; }

        [TableField]
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
