using CoffeeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Utils
{
    public interface IWebSecurity
    {
        public string UserId { get; }
        public string Role { get; }
        public bool HasRole(string role);
        public bool InRole(string role);
        public User User { get; }
    }
}
