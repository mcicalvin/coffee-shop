using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Models.Requests
{
    public class ClientRequest : BaseRequest
    {
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string Email { get;  set; }
        public string Address { get;  set; }
        public string IdNumber { get;  set; }
    }
}
