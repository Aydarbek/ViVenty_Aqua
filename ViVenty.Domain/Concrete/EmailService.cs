using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Configuration;
using ViVenty.Domain.Abstract;
using ViVenty.Domain.Entities;

namespace ViVenty.Domain.Concrete
{
    public class EmailService : IEmailService
    {
        private MailAddress MailTo;
        private MailAddress MailFrom;
        private string MailSubject;


        public void SendOrderDetailsToAdmin(Cart cart, Order order)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                GetSmtpClientCredentials(smtpClient);

                MailTo = new MailAddress("a88236 @gmail.com", "Admin");
                MailFrom = new MailAddress("aidar_ahmetshin@mail.ru", "ViVenty Aqua Web Store");
                MailSubject = "Получен заказ № " + order.Id.ToString();


                StringBuilder body = new StringBuilder().
                    AppendLine("Новый заказ обработан").
                    AppendLine("----------------------------------------").
                    AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Hsuit.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2} р.)\n",
                        line.Quantity, line.Hsuit.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0} р.\n", cart.ComputeTotalValue()).
                    AppendLine("---------------------------------------").
                    AppendLine("Доставка:").
                    AppendLine(order.Name).
                    AppendLine(order.Phone).
                    AppendLine(order.Email).
                    AppendLine(order.Address);

                MailMessage mailMessage = new MailMessage(MailFrom, MailTo);
                mailMessage.Subject = MailSubject;
                mailMessage.Body = body.ToString();

                smtpClient.Send(mailMessage); 
            }
        }

        public void SendOrderDetailsToClient(Cart cart, Order order)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                GetSmtpClientCredentials(smtpClient);

                MailTo = new MailAddress(order.Email, order.Name);
                MailFrom = new MailAddress("aidar_ahmetshin@mail.ru", "ViVenty Aqua Web Store");
                MailSubject = "Ваш заказ номер " + order.Id + " принят";


                StringBuilder body = new StringBuilder().
                    AppendLine("Ваш заказ номер " + order.Id + " принят").
                    AppendLine("----------------------------------------").
                    AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Hsuit.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2} р.)\n",
                        line.Quantity, line.Hsuit.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0} р.\n", cart.ComputeTotalValue()).
                    AppendLine("---------------------------------------").
                    AppendLine("Доставка:").
                    AppendLine(order.Name).
                    AppendLine(order.Phone).
                    AppendLine(order.Email).
                    AppendLine(order.Address).
                    AppendLine("--------------------------------------").
                    AppendLine("Оператор свяжется с вами для уточнения времени доставки");

                MailMessage mailMessage = new MailMessage(MailFrom, MailTo);
                mailMessage.Subject = MailSubject;
                mailMessage.Body = body.ToString();

                smtpClient.Send(mailMessage);
            }
        }

        private static void GetSmtpClientCredentials(SmtpClient smtpClient)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            smtpClient.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
        }
    }
}
