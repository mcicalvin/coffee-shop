using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Interfaces.Services.QueryService;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using CoffeeShop.Domain.Models.Responses.QueryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Services.QueryServices
{
    public interface IClientQueryService : IBaseQueryService<ClientFilter, Client>
    {
        ObjectListResponse<ClientResponse> Filter(ClientFilter filter);
        ObjectResponse<ClientResponse> Get(long id);
    }
}
