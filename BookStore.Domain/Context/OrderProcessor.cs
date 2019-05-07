using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace BookStore.Domain.Context
{
    public class OrderProcessor : IOrderProcessor
    {
        private EmailSetting emailSetting;
        public OrderProcessor(EmailSetting _emailSetting)
        {
            emailSetting = _emailSetting;
        }
        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using(var SmtpClient = new SmtpClient())
            {
                SmtpClient.EnableSsl = emailSetting.useSSL;
                SmtpClient.Host = emailSetting.serverName;
                SmtpClient.Port = emailSetting.serverPort;
                SmtpClient.UseDefaultCredentials = false;
                SmtpClient.Credentials = new NetworkCredential(
                    emailSetting.userName, emailSetting.password);
                if (emailSetting.writeAsFile)
                {
                    SmtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    SmtpClient.PickupDirectoryLocation = emailSetting.fileLocation;
                    SmtpClient.EnableSsl = false;
                }
                StringBuilder stBuilder = new StringBuilder()
                .AppendLine("A new order has been submitted")
                .AppendLine("------------------------------")
                .AppendLine("Books:");
                foreach (var cLine in cart.CartLines)
                {
                    var subTotal = cLine.Book.Price * cLine.Quantity;
                    stBuilder.AppendFormat("{0} x {1} = {2:c}", cLine.Quantity, cLine.Book.Title, subTotal);


                }
                stBuilder.AppendFormat("Total order value {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---------")
                    .AppendLine("Shipped to:")
                    .AppendLine(shippingDetails.name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2)
                    .AppendLine(shippingDetails.state)
                    .AppendLine(shippingDetails.city)
                    .AppendLine(shippingDetails.country)
                    .AppendLine("------")
                    .AppendFormat("Gift wrap:{0}", shippingDetails.giftWrap ? "Yes" : "No");

                MailMessage message = new MailMessage(
                    from: emailSetting.mailFromAddress,
                    to: emailSetting.mailToAddress,
                    subject: "New order submitted",
                    body: stBuilder.ToString());

                if (emailSetting.writeAsFile)
                    message.BodyEncoding = Encoding.ASCII;
                try
                {
                    SmtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
                
            }
        }
    }
}
