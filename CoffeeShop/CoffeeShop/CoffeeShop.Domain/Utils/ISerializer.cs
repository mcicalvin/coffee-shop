using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Utils
{
    public interface ISerializer
    {
        string Serialize(object data);
        T? Deserialize<T>(string data);
    }
}
