using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Repository;
using Shared.DTOs;
using Shared.Models;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrder orderService) : ControllerBase
    {

        [Host]
        public async Task<ActionResult<ServiceResponse>> AddOrder(Order order)
        {
            var response = await orderService.AddOrderAsync(order);
            return response.Flag ? Ok(response) : BadRequest(response);
        }

    }
}
