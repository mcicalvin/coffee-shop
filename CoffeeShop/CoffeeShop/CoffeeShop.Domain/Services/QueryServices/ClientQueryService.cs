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
    public class ClientQueryService : BaseQueryService<ClientFilter, Client>, IClientQueryService
    {

        public ClientQueryService(IQueryRepository<Client, ClientFilter> queryRepository,
           ILogger<BaseService> logger) : base(queryRepository, logger)
        {

        }

        public override string ServiceName => nameof(ClientQueryService);

        public ObjectResponse<ClientResponse> Get(long id)
        {
            var response = new ObjectResponse<ClientResponse>();
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

        public ObjectListResponse<ClientResponse> Filter(ClientFilter filter)
        {
            var response = new ObjectListResponse<ClientResponse>();
            try
            {
                var clients = queryRepository
                    .Filter(filter)
                    .ToList();

                var mapped = clients.Map();

                var overallPoints = mapped.Sum(x => x.Points);

                response.Data = mapped;
                response.TotalSum = overallPoints.ToString();
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
