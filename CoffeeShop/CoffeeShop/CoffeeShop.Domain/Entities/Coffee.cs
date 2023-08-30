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
    public class Coffee : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public static Coffee Create(CoffeeRequest request)
        {
            return new Coffee
            {
                ImageUrl = request.ImageUrl,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
