using Microsoft.EntityFrameworkCore;
using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Interfaces.Repositories;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infrastructure.Repositories.BaseRepositories
{
    public abstract class QueryRepository<Ent, Fil> : IQueryRepository<Ent, Fil> where Ent : BaseEntity where Fil : BaseFilter
    {
        protected readonly CoffeeShopDbContext context;

        public QueryRepository(CoffeeShopDbContext context)
        {
            this.context = context;
        }

        /*
        public Ent GetById(long id)
        {
            var entity = context.Set<Ent>()
                .AsNoTracking()
                .Where(e => e.Status == EntityStatus.Active)
                .FirstOrDefault(e => e.Id == id);

            return entity;
        }
        */

        public Ent GetById(long id)
        {
            //var entity = context.Set<Ent>()
            //    .AsNoTracking()
            //    .Where(e => e.Status == EntityStatus.Active)
            //    .FirstOrDefault(e => e.Id == id);
            var entity = Get(id);
            entity = SpecifyInclude(entity);
            return entity;
        }

        protected abstract Ent Get(long id);

        public IQueryable<Ent> GetAll()
        {
            var items = context.Set<Ent>()
                    .AsNoTracking()
                    .AsQueryable();

            return items;
        }

        public IQueryable<Ent> Filter(Fil filter)
        {
            var results = FilterData(filter);

            results = SpecifyInclude(results);

            return results;
        }

        protected abstract IQueryable<Ent> FilterData(Fil filter);

        protected virtual Ent SpecifyInclude(Ent entity)
        {
            return entity;
        }

        protected virtual IQueryable<Ent> SpecifyInclude(IQueryable<Ent> query)
        {
            return query;
        }
    }
}
