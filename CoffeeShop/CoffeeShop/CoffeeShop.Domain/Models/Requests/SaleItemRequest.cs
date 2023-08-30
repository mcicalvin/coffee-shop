using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Models.Requests
{
    public class SaleItemRequest : BaseRequest
    {
        public int Qty { get; set; }
    }
}
