using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using CoffeeShop.Domain.Models.Responses.QueryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Services.QueryService
{
    public interface IBaseQueryService<Fil, Ent> where Fil : BaseFilter where Ent : BaseEntity
    {
        RawObjectResponse<Ent> GetById(long id);
        RawObjectListResponse<Ent> Filter(Fil filter);
    }
}
