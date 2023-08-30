using Microsoft.EntityFrameworkCore;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Infrastructure.DbContexts;
using CoffeeShop.Infrastructure.Repositories.BaseRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infrastructure.Repositories
{
    public class ClientQueryRepository : QueryRepository<Client, ClientFilter>,
         IQueryRepository<Client, ClientFilter>
    {
        public ClientQueryRepository(CoffeeShopDbContext context) : base(context)
        {

        }

        protected override IQueryable<Client> FilterData(ClientFilter filter)
        {
            var data = context.Set<Client>()
              .Where(x => x.Status == EntityStatus.Active)
              .Include(p => p.Point)
              .AsQueryable();

            return data;
        }

        protected override Client Get(long id)
        {
            var entity = context.Set<Client>()
              .AsNoTracking()
                .Include(p => p.Point)
              .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

            return entity;
        }
    }
}
