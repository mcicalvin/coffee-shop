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
    public class CoffeeCommandService : BaseCommandService<Coffee>, ICoffeeCommandService
    {

        private readonly IQueryRepository<Coffee, CoffeeFilter> queryRepository;

        public CoffeeCommandService(ICommandRepository<Coffee> commandRepository,
           ILogger<BaseCommandService<Coffee>> logger, IQueryRepository<Coffee, CoffeeFilter> queryRepository) : base(commandRepository, logger)
        {
            this.queryRepository = queryRepository;
        }

        public override string ServiceName => nameof(CoffeeCommandService);

        public BaseResponse Add(CoffeeRequest request)
        {
            Coffee coffee = Coffee
                .Create(request);

            return Create(coffee);
        }

        public override void AfterCreation(Coffee entity)
        {
           
        }

        public BaseResponse Delete(BaseRequest request)
        {
            var coffee = queryRepository.GetById(request.Id.Value);

            if (coffee == null)
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "Not found"
                };

            coffee.UpdatedAt = DateTime.Now;
            var response = Delete(coffee);

            return response;
        }

        public BaseResponse Edit(CoffeeRequest request)
        {
            var coffee = queryRepository.GetById(request.Id.Value);

            if (coffee == null)
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "Not found"
                };

            coffee.UpdatedAt = DateTime.Now;
            coffee.Name = request.Name;
            coffee.Description = request.Description;
            coffee.Price = request.Price;
            coffee.ImageUrl = request.ImageUrl;
            var response = Update(coffee);

            return response;
        }
    }
}
