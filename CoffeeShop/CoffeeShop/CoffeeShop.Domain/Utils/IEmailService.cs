using CoffeeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Utils
{
    public interface IEmailService
    {
        void Send(Email email);
    }
}
