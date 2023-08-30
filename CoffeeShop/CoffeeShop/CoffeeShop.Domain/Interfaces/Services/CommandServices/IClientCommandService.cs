using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Services.CommandServices
{
    public interface IClientCommandService : IBaseCommandService<Client>
    {
        BaseResponse Add(ClientRequest request);
        BaseResponse Edit(ClientRequest request);
        BaseResponse Delete(BaseRequest request);
    }
}
