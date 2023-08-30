using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Enums
{
    public enum ResponseStatus
    {
        Success = 1,
        Fail,
        Unauthorized,
        UserExist,
        NoneExist
    }
}
