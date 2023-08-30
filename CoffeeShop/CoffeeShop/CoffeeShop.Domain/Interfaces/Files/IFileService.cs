using CoffeeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Interfaces.Files
{ 
    public interface IFileService
    {
      
        List<FileInfo> ReadFiles(string directory);
 
    }
}
