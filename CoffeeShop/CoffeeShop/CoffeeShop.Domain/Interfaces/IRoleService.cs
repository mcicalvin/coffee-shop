using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces
{
    public interface IRoleService
    {
        ObjectListResponse<RoleResponse> Filter(RoleFilter filter);
    }
}
