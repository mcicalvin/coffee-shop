using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Mappers
{
    public static class UserMapper
    {
        public static UserResponse Map(this User entity)
        {
            return new UserResponse
            {

            };
        }

        public static List<UserResponse> Map(this List<User> entities)
        {
            var mapped = new List<UserResponse>();

            foreach (var item in entities)
            {
                mapped.Add(Map(item));
            }

            return mapped;
        }
    }
}
