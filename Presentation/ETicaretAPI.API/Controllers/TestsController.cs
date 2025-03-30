using ETicaretAPI.Application.Abstractions.Sms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TestsController(ISmsService smsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTest()
        {
            //var result = await smsService.SendSmsAsync("5555555555", "Test", "Test");
            //Console.WriteLine(result);
            return Ok();
        }
    }
}
