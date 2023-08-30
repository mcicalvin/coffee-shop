using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Models.Responses.QueryResponse
{
    public class ClientResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }
        public double Points { get; set; }
        public string Value { get; set; }
    }
}
