using CoffeeShop.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public long ClientId { get; set; }
        public Client Client { get; set; }

        public List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();

        public static Sale Create(long clientId, SaleRequest request, List<SaleItem> saleItems)
        {
            return new Sale
            {
                ClientId = clientId,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                SaleItems = saleItems,
                Status = Enums.EntityStatus.Active

            };
        }
    }
}
