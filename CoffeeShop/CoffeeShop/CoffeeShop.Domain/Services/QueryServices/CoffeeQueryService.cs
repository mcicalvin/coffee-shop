using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Interfaces.Services.QueryServices;
using CoffeeShop.Domain.Mappers;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using CoffeeShop.Domain.Models.Responses.QueryResponse;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Services.QueryServices
{
    public class CoffeeQueryService : BaseQueryService<CoffeeFilter, Coffee>, ICoffeeQueryService
    {

        public CoffeeQueryService(IQueryRepository<Coffee, CoffeeFilter> queryRepository,
           ILogger<BaseService> logger) : base(queryRepository, logger)
        {

        }

        public override string ServiceName => nameof(CoffeeQueryService);

        public ObjectResponse<CoffeeResponse> Get(long id)
        {
            var response = new ObjectResponse<CoffeeResponse>();
            try
            {
                var coffee = queryRepository.GetById(id);

                var mapped = coffee.Map();

                response.Data = mapped;
                response.StatusCode = Enums.ResponseStatus.Success;
            }
            catch(Exception ex)
            {
                response.StatusCode = Enums.ResponseStatus.Fail;
            }

            return response;


        }

        public ObjectListResponse<CoffeeResponse> Filter(CoffeeFilter filter)
        {
            var response = new ObjectListResponse<CoffeeResponse>();
            try
            {
                var coffees = queryRepository
                    .Filter(filter)
                    .ToList();

                var mapped = coffees.Map();

                response.Data = mapped;
                response.StatusCode = Enums.ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                response.StatusCode = Enums.ResponseStatus.Fail;
            }

            return response;
        }
    }
}
