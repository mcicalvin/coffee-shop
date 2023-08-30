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
    public class PointQueryRepository : QueryRepository<Point, PointFilter>,
         IQueryRepository<Point, PointFilter>
    {
        public PointQueryRepository(CoffeeShopDbContext context) : base(context)
        {

        }

        protected override IQueryable<Point> FilterData(PointFilter filter)
        {
            var data = context.Set<Point>()
              .Where(x => x.Status == EntityStatus.Active)
              .AsQueryable();

            return data;
        }

        protected override Point Get(long id)
        {
            var entity = context.Set<Point>()
              .AsNoTracking()
              .FirstOrDefault(x => x.Id == id && x.Status == EntityStatus.Active);

            return entity;
        }
    }
}
