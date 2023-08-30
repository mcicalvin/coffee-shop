using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Interfaces.Services.CommandServices;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Services.CommandServices
{
    public class ClientCommandService : BaseCommandService<Client>, IClientCommandService
    {

        private readonly IQueryRepository<Client, ClientFilter> queryRepository;

        public ClientCommandService(ICommandRepository<Client> commandRepository,
           ILogger<BaseCommandService<Client>> logger, IQueryRepository<Client, ClientFilter> queryRepository) : base(commandRepository, logger)
        {
            this.queryRepository = queryRepository;
        }

        public override string ServiceName => nameof(ClientCommandService);

        public BaseResponse Add(ClientRequest request)
        {
            Client client = Client
                .Create(request);

            return Create(client);
        }

        public override void AfterCreation(Client entity)
        {
           
        }

        public BaseResponse Delete(BaseRequest request)
        {
            var client = queryRepository.GetById(request.Id.Value);

            if (client == null)
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "Not found"
                };

            client.UpdatedAt = DateTime.Now;
            var response = Delete(client);

            return response;
        }

        public BaseResponse Edit(ClientRequest request)
        {
            var client = queryRepository.GetById(request.Id.Value);

            if (client == null)
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "Not found"
                };

            client.UpdatedAt = DateTime.Now;
            client.FirstName = request.FirstName;
            client.LastName = request.LastName;
            client.Email = request.Email;
            var response = Update(client);

            return response;
        }
    }
}
