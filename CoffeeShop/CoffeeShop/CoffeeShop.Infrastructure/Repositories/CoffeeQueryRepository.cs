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
    public class CoffeeQueryRepository : QueryRepository<Coffee, CoffeeFilter>,
         IQueryRepository<Coffee, CoffeeFilter>
    {
        public CoffeeQueryRepository(CoffeeShopDbContext context) : base(context)
        {

        }

        protected override IQueryable<Coffee> FilterData(CoffeeFilter filter)
        {
            var data = context.Set<Coffee>()
              .Where(x => x.Status == EntityStatus.Active)
              .AsQueryable();

            return data;
        }

        protected override Coffee Get(long id)
        {
            var entity = context.Set<Coffee>()
              .AsNoTracking()
              .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

            return entity;
        }
    }
}
