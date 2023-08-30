using CoffeeShop.Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdNumber { get; set; }

        public Point? Point { get; set; }

        public static Client Create(ClientRequest request)
        {
            return new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = request.Address,
                IdNumber = request.IdNumber,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
             Status = Enums.EntityStatus.Active
            };
        }
    }
}
