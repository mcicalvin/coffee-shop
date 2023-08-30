using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using CoffeeShop.Domain.Interfaces.Files;
using CoffeeShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Infrastructure.Files
{
    public class FileService : IFileService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<FileService> logger;

        public FileService(IConfiguration configuration, ILogger<FileService> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

  
        public List<FileInfo> ReadFiles(string directory)
        {
            try
            {
                return new DirectoryInfo(directory)
                .GetFiles("*.*", SearchOption.AllDirectories).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError("Directory: " + directory);
                logger.LogError("1023: " + ex.Message);
                return new List<FileInfo>();
            }
        }
    }
}
