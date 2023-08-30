using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.UseCases
{
    public class CalculatePoints
    {
        private const int ThreshHoldNumber = 10;
        private List<SaleItem> SaleItems = new List<SaleItem>();

        public double Calculate { get { return GetPoints(); } }

        private double GetPoints()
        {
            double total = 0.00D;

            var numberOfCoffeesBought = SaleItems.Sum(x => x.Qty);

            if (numberOfCoffeesBought > 0)
            {
                total = ((double)numberOfCoffeesBought / (double)ThreshHoldNumber);
            }

            return total;
        }

        public static CalculatePoints AddItems(List<SaleItem> saleItems)
        {
            return new CalculatePoints
            {
                SaleItems = saleItems,

            };
        }
    }
}
