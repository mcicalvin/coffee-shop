using Microsoft.AspNetCore.Identity;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Interfaces;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public ObjectListResponse<RoleResponse> Filter(RoleFilter filter)
        {
            var response = new ObjectListResponse<RoleResponse>();

            var rolseResponse = new List<RoleResponse>();
            var roles = roleManager.Roles.ToList();

            foreach (var role in roles)
            {
                rolseResponse.Add(new RoleResponse
                {
                    Id = role.Id,
                    Name = role.Name,
                });
            }

            response.Data = rolseResponse;
            response.StatusCode = ResponseStatus.Success;
            return response;
        }
    }
}
