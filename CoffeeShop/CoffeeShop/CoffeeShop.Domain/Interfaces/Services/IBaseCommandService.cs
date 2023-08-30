using CoffeeShop.Domain.Entities;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Services
{
    public interface IBaseCommandService<Ent> where Ent : BaseEntity
    {
        CreationResponse<Ent> Create(Ent entity);
        BaseResponse Update(Ent entity);
        BaseResponse Delete(long id);
        BaseResponse Delete(Ent entity);
    }
}
