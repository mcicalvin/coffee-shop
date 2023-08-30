using CoffeeShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Models.Responses
{
    public class BaseResponse
    {
        public ResponseStatus StatusCode { get; set; }
        public string Status { get { return StatusCode.ToString().ToLower(); } }
        public string Message { get; set; } = string.Empty;
        public string TotalSum { get; set; } = string.Empty;
        public string TotalCount { get; set; } = string.Empty;
    }
}
