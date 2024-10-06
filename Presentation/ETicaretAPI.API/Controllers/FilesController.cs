using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class FilesController(IConfiguration configuration) : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetBaseStorageUrl()
        {
            return Ok(new
            {
                Url = configuration["BaseStorageUrl"]
            });
        }

    }
}
