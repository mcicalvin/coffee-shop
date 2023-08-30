using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Interfaces.Services.CommandServices;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Responses;
using CoffeeShop.Domain.UseCases;
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
    public class SaleCommandService : BaseCommandService<Sale>, ISaleCommandService
    {

        private readonly IQueryRepository<Sale, SaleFilter> queryRepository;
        private readonly IQueryRepository<Client, ClientFilter> clientQueryRepository;
        private readonly IQueryRepository<Point, PointFilter> pointQueryRepository;
        private readonly ICommandRepository<Point> pointCommandRepo;

        public SaleCommandService(ICommandRepository<Sale> commandRepository, ICommandRepository<Point> pointCommandRepo,
            IQueryRepository<Point, PointFilter> pointQueryRepository, IQueryRepository<Client, ClientFilter> clientQueryRepository,
           ILogger<BaseCommandService<Sale>> logger, IQueryRepository<Sale, SaleFilter> queryRepository) : base(commandRepository, logger)
        {
            this.queryRepository = queryRepository;
            this.pointCommandRepo = pointCommandRepo;
            this.pointQueryRepository = pointQueryRepository;
            this.clientQueryRepository = clientQueryRepository;
        }

        public override string ServiceName => nameof(SaleCommandService);

        public BaseResponse Add(SaleRequest request)
        {
            var sales = new List<SaleItem>();

            var client = clientQueryRepository.GetAll()
               .FirstOrDefault(x => x.IdNumber == request.IdNumber);

            if(client == null)
            {
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "This IdNumber does exist on our database, please add the client"
                };
            }

            if (request.CoffeeItems.Any())
            {
                foreach(var coffeeRequest in request.CoffeeItems)
                {
                    sales.Add(SaleItem.Create(coffeeRequest));
                }
            }

           

            Sale sale = Sale
                .Create(client.Id, request, sales);

            return Create(sale);
        }

        public override void AfterCreation(Sale sale)
        {
            if (sale.SaleItems.Any())
            {
                var points = CalculatePoints.AddItems(sale.SaleItems).Calculate;

                var clientPoint = pointQueryRepository
                    .GetAll()
                    .FirstOrDefault(x => x.ClientId == sale.ClientId);

                if (clientPoint != null)
                {
                    clientPoint.AccumulatedPoint += points;
                    pointCommandRepo.Update(clientPoint);
                }
                else
                {
                    var point = Point.Create(sale.ClientId, points);
                    pointCommandRepo.Create(point);
                }

            }
        }

        public BaseResponse Delete(BaseRequest request)
        {
            var sale = queryRepository.GetById(request.Id.Value);

            if (sale == null)
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "Not found"
                };

            sale.UpdatedAt = DateTime.Now;
            var response = Delete(sale);

            return response;
        }

        public BaseResponse Edit(SaleRequest request)
        {
            var sale = queryRepository.GetById(request.Id.Value);

            if (sale == null)
                return new BaseResponse
                {
                    StatusCode = Enums.ResponseStatus.Fail,
                    Message = "Not found"
                };

            sale.UpdatedAt = DateTime.Now;
    
            var response = Update(sale);

            return response;
        }
    }
}
