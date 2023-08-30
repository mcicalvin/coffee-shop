using CoffeeShop.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public long SaleId { get; set; }
        public long CoffeeId { get; set; }
        public int Qty { get; set; }

        public static SaleItem Create(SaleItemRequest request)
        {
            return new SaleItem
            {
                CoffeeId = request.Id.Value,
                Qty = request.Qty,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Enums.EntityStatus.Active
            };
        }
    }
}
