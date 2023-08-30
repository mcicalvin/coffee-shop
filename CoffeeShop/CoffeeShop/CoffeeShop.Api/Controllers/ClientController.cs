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
    public class ClientController : ControllerBase
    {
        private readonly IClientCommandService clientCommandService;
        private readonly IClientQueryService clientQueryService;

        public ClientController(IClientCommandService clientCommandService,
            IClientQueryService clientQueryService)
        {
            this.clientCommandService = clientCommandService;
            this.clientQueryService = clientQueryService;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ClientRequest request)
        {
            var res = clientCommandService.Add(request);
            return Ok(res);
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromBody] BaseRequest request)
        {
            var res = clientCommandService.Delete(request);
            return Ok(res);
        }


        [HttpPut("update")]
        public IActionResult Update([FromBody] ClientRequest request)
        {
            var res = clientCommandService.Edit(request);
            return Ok(res);
        }


        [HttpGet("filter")]
        public IActionResult Filter([FromQuery] ClientFilter filter)
        {
            var res = clientQueryService.Filter(filter);
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById( long? id)
        {
            if (id == null)
                return NotFound();

            var res = clientQueryService.Get(id.Value);
            return Ok(res);
        }
    }
}
