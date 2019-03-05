using System.Text;
using ViVenty.Domain.Entities;
using ViVenty.Domain.Abstract;
using System.Net;
using System.Net.Mail;

namespace ViVenty.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddress = "aidar_ahmetshin@mail.ru";
        public string MailFromAddress = "a88236@gmail.com";
        public bool UseSsl = true;
        public string UserName = "a88236@gmail.com";
        public string Password = "Fhpfvfc-16";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"D:\Projects\ASP.NET\ViVenty_Store\Orders_mails";
    }


    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor (EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials =
                    new NetworkCredential(emailSettings.UserName, emailSettings.Password);


                StringBuilder body = new StringBuilder().
                    AppendLine("Новый заказ обработан").
                    AppendLine("---------------------").
                    AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Hsuit.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c})\n",
                        line.Quantity, line.Hsuit.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}\n", cart.ComputeTotalValue()).
                    AppendLine("--------------------").
                    AppendLine("Доставка:").
                    AppendLine(shippingDetails.Name).
                    AppendLine(shippingDetails.Phone).
                    AppendLine(shippingDetails.Email).
                    AppendLine(shippingDetails.Address);

                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "Новый заказ отправлен!",
                    body.ToString());

                if (emailSettings.WriteAsFile)
                    mailMessage.BodyEncoding = Encoding.UTF8;

                smtpClient.Send(mailMessage);
            }
        }
    }
}
