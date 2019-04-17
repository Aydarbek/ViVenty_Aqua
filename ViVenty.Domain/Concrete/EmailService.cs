using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
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
            MailTo = new MailAddress("aidar_ahmetshin@mail.ru", "Admin");
            MailFrom = new MailAddress("a88236@gmail.com", "ViVenty Aqua Web Store");
            MailSubject = "Получен заказ № " + order.Id.ToString();

            using (var smtpClient = new SmtpClient())
            {
                StringBuilder body = new StringBuilder().
                    AppendLine("Новый заказ обработан").
                    AppendLine("----------------------------------------").
                    AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Hsuit.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c})\n",
                        line.Quantity, line.Hsuit.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}\n", cart.ComputeTotalValue()).
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
            MailTo = new MailAddress(order.Email, order.Name);
            MailFrom = new MailAddress("a88236@gmail.com", "ViVenty Aqua Web Store");
            MailSubject = "Ваш заказ номер " + order.Id + " принят";

            using (var smtpClient = new SmtpClient())
            {

                StringBuilder body = new StringBuilder().
                    AppendLine("Ваш заказ номер " + order.Id + " принят").
                    AppendLine("----------------------------------------").
                    AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Hsuit.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c})\n",
                        line.Quantity, line.Hsuit.Name, subtotal);
                }

                body.AppendFormat("Общая стоимость: {0:c}\n", cart.ComputeTotalValue()).
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
    }
}
