using CoffeeShop.Domain.Interfaces.Services.CommandServices;
using CoffeeShop.Domain.Interfaces.Services.QueryServices;
using CoffeeShop.Domain.Models.Requests.Filters;
using CoffeeShop.Domain.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleCommandService saleCommandService;
        private readonly ICoffeeQueryService coffeeQueryService;

        public SaleController(ISaleCommandService saleCommandService,
            ICoffeeQueryService coffeeQueryService)
        {
            this.saleCommandService = saleCommandService;
            this.coffeeQueryService = coffeeQueryService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] SaleRequest request)
        {
            var res = saleCommandService.Add(request);
            return Ok(res);
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] BaseRequest request)
        {
            var res = saleCommandService.Delete(request);
            return Ok(res);
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] SaleRequest request)
        {
            var res = saleCommandService.Edit(request);
            return Ok(res);
        }


    }
}
