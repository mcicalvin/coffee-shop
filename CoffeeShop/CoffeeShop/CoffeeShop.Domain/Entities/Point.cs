using CoffeeShop.Domain.Models.Requests;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Entities
{
    public class Point : BaseEntity
    {
        private const int ThirtyPointsWorthOneRand = 30;
        public long ClientId { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public double AccumulatedPoint { get; set; }
        public decimal PointMonetaryValue { 
            get 
            {
                var value = Math.Round(Convert.ToDecimal(MonetaryAmount()),2);
                return value; 
            } 
        }

        private double MonetaryAmount() => 
            (AccumulatedPoint > 0 ? ((double)AccumulatedPoint / (double)ThirtyPointsWorthOneRand) : 0);

        public static Point Create(long clientId, double accumulatedPoint)
        {
            return new Point
            {
                ClientId = clientId,
                AccumulatedPoint = accumulatedPoint,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Status = Enums.EntityStatus.Active
            };
        }
    }
}
