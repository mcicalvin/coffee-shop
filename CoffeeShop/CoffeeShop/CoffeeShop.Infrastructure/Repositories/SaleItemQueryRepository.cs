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
using CoffeeShop.Domain.Models.Requests;

namespace CoffeeShop.Infrastructure.Repositories
{
    public class SaleItemQueryRepository : QueryRepository<SaleItem, SaleItemFilter>,
         IQueryRepository<SaleItem, SaleItemFilter>
    {
        public SaleItemQueryRepository(CoffeeShopDbContext context) : base(context)
        {
        }

        protected override IQueryable<SaleItem> FilterData(SaleItemFilter filter)
        {
            var data = context.Set<SaleItem>()
              .Where(x => x.Status == EntityStatus.Active)
              .AsQueryable();

            return data;
        }

        protected override SaleItem Get(long id)
        {
            var entity = context.Set<SaleItem>()
              .AsNoTracking()
              .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

            return entity;
        }
    }
}
