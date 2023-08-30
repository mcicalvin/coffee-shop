using Microsoft.Extensions.Logging;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Interfaces.Services.QueryService;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using CoffeeShop.Domain.Models.Responses.QueryResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Services.QueryServices
{
    public abstract class BaseQueryService<Fil, Ent> : BaseService, IBaseQueryService<Fil, Ent> 
        where Fil : BaseFilter where Ent : BaseEntity
    {
        protected IQueryRepository<Ent, Fil> queryRepository;
        protected BaseQueryService(IQueryRepository<Ent, Fil> queryRepository, 
            ILogger<BaseService> logger) : base(logger)
        {
            this.queryRepository = queryRepository;
        }

        public RawObjectListResponse<Ent> Filter(Fil filter)
        {
            var response = new RawObjectListResponse<Ent>();

            try
            {
                var results = queryRepository.Filter(filter);
               
                response.Data = results;
                response.StatusCode = Enums.ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                LogError(response, ex, "Error occurred when filtering data.");
            }
            return response;
        }

        public RawObjectResponse<Ent> GetById(long id)
        {
            var response = new RawObjectResponse<Ent>();

            try
            {
                var results = queryRepository.GetById(id);

                response.Data = results;
                response.StatusCode = Enums.ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                LogError(response, ex, "Error occurred reading data.");
            }
            return response;
        }
    }
}
