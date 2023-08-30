using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Requests.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Repositories
{
    public interface ICommandRepository<Ent> where Ent : BaseEntity
    {
        Ent Create(Ent entity);
        Ent GetById(long id);
        bool Delete(long id, bool isPermanetDelete = false);
        bool Delete(Ent entity, bool isPermanetDelete = false);
        Ent Update(Ent entity);
    }
}
