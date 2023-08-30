using CoffeeShop.Domain.Interfaces.Services.CommandServices;
using CoffeeShop.Domain.Interfaces.Services.QueryServices;
using CoffeeShop.Domain.Models.Requests;
using CoffeeShop.Domain.Models.Requests.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {

        private readonly ICoffeeCommandService coffeeCommandService;
        private readonly ICoffeeQueryService coffeeQueryService;

        public CoffeeController(ICoffeeCommandService coffeeCommandService,
            ICoffeeQueryService coffeeQueryService)
        {
            this.coffeeCommandService = coffeeCommandService;
            this.coffeeQueryService = coffeeQueryService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CoffeeRequest request)
        {
            var res = coffeeCommandService.Add(request);
            return Ok(res);
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] BaseRequest request)
        {
            var res = coffeeCommandService.Delete(request);
            return Ok(res);
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] CoffeeRequest request)
        {
            var res = coffeeCommandService.Edit(request);
            return Ok(res);
        }


        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] CoffeeFilter filter)
        {
            var res = coffeeQueryService.Filter(filter);
            return Ok(res);
        }
    }
}
