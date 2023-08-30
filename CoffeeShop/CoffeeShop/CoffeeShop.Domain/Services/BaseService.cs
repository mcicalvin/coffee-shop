using Microsoft.Extensions.Logging;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Services
{
    public abstract class BaseService
    {
        protected ILogger<BaseService> logger;

        public BaseService(ILogger<BaseService> logger)
        {
            this.logger = logger;
        }

        public abstract string ServiceName { get; }

        protected void LogError(BaseResponse result, Exception ex, string message)
        {
            result.StatusCode = Enums.ResponseStatus.Fail;
            result.Message = result.Message;
            this.logger.LogError(ex, ex.Message + $"{ServiceName}");
        }
    }
}
