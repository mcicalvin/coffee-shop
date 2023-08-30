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
    public interface ICoffeeCommandService : IBaseCommandService<Coffee>
    {
        BaseResponse Add(CoffeeRequest request);
        BaseResponse Edit(CoffeeRequest request);
        BaseResponse Delete(BaseRequest request);
    }
}
