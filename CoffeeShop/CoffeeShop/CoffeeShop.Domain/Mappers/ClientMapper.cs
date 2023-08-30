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
    public static class ClientMapper
    {
        public static ClientResponse Map(this Client entity)
        {
            return new ClientResponse
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Address = entity.Address,
                Points = entity.Point == null ? 0.00D : entity.Point?.AccumulatedPoint?? 0.00D,
                Value = (entity.Point == null) ? 0D.ToRand() : entity.Point?.PointMonetaryValue.ToRand()
            };
        }

        public static List<ClientResponse> Map(this List<Client> entities)
        {
            var mapped = new List<ClientResponse>();

            foreach (var item in entities)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }
    }
}
