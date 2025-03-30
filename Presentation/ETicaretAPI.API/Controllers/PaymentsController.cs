using ETicaretAPI.Application.Features.Commands.Payment.CreatePayment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<ActionResult> PaymentOrder([FromBody] CreatePaymentCommandRequest createPaymentCommandRequest)
        {
            CreatePaymentCommandResponse response = await mediator.Send(createPaymentCommandRequest);
            return Ok(response);
        }
    }
}
