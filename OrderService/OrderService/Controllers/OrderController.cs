using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Models;
using OrderService.Business;
using OrderService.Business.Commands;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("order")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromBody] OrderRequest request,
            [FromServices] ICreateOrder command)
        {
            return Ok(await command.ExecuteAsync(request));
        }
    }
}