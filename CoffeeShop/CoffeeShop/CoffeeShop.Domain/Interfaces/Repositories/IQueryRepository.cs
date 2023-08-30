using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Repositories
{
    //public interface IQueryRepository<Ent> where Ent : BaseEntity
    public interface IQueryRepository<Ent, Fil> where Ent : BaseEntity where Fil : BaseFilter
    {

        IQueryable<Ent> Filter(Fil filter);
        Ent GetById(long id);
        IQueryable<Ent> GetAll();
    }
}
