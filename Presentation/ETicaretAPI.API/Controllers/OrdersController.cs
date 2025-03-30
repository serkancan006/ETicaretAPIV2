using ETicaretAPI.Application.Features.Commands.Order.CompleteOrder;
using ETicaretAPI.Application.Features.Commands.Order.CreateOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetAllOrders;
using ETicaretAPI.Application.Features.Queries.Order.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{Id}")]
        public async Task<ActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest getOrderByIdQueryRequest)
        {
            GetOrderByIdQueryResponse response = await mediator.Send(getOrderByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllOrders([FromQuery] GetAllOrdersQueryRequest getAllOrdersQueryRequest)
        {
            GetAllOrdersQueryResponse response = await mediator.Send(getAllOrdersQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        // siparişlerin tamamlanması için kurye tarafından kullanılacak veya kargo şirketi tarafından kullanılacak bir endpoint ödeme ve teslimin bittiğini gösterir
        [HttpGet("complete-order/{Id}")]
        public async Task<ActionResult> CompleteOrder([FromRoute] CompleteOrderCommandRequest completeOrderCommandRequest)
        {
            CompleteOrderCommandResponse response = await mediator.Send(completeOrderCommandRequest);
            return Ok(response);
        }

    

    }
}
