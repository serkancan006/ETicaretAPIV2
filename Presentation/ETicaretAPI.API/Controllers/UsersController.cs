using ETicaretAPI.Application.Features.Commands.AppUser.CreateUser;
using ETicaretAPI.Application.Features.Commands.AppUser.UpdatePassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }
    }
}
