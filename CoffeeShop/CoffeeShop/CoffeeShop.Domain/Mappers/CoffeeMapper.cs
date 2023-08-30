using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Extensions;
using CoffeeShop.Domain.Models.Responses;
using CoffeeShop.Domain.Models.Responses.QueryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Mappers
{
    public static class CoffeeMapper
    {
        public static CoffeeResponse Map(this Coffee entity)
        {
            return new CoffeeResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price.ToRand(),
                ImageUrl = entity.ImageUrl,
            };
        }

        public static List<CoffeeResponse> Map(this List<Coffee> entities)
        {
            var mapped = new List<CoffeeResponse>();

            foreach (var item in entities)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }
    }
}
