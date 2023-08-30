using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Models.Requests
{
    public class SaleRequest : BaseRequest
    {
        public string IdNumber { get; set; }
        public List<SaleItemRequest> CoffeeItems { get; set; } = new List<SaleItemRequest>();
    }
}
