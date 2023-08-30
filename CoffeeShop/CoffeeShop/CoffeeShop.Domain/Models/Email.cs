using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Models
{
    public class Email
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public static Email Create(Dictionary<string, string> to, string subject, string message)
        {
            var tos = new List<MailboxAddress>();
            tos.AddRange(to.Select(x => new MailboxAddress(x.Value, x.Key)));

            return new Email
            {
                To = tos,
                Subject = subject,
                Message = message
            };
        }
    }
}
