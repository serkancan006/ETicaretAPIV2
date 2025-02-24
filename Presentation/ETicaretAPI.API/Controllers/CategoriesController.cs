using ETicaretAPI.Application.Features.Commands.Category.CreateMainCategory;
using ETicaretAPI.Application.Features.Commands.Category.CreateSubCategory;
using ETicaretAPI.Application.Features.Queries.Category.GetAllCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllCategory([FromQuery] GetAllCategoryQueryRequest getAllCategoryQueryRequest)
        {
            List<GetAllCategoryQueryResponse> response = await mediator.Send(getAllCategoryQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMainCategory([FromBody] CreateMainCategoryCommandRequest createMainCategoryCommandRequest)
        {
            await mediator.Send(createMainCategoryCommandRequest);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> CreateSubCategory([FromBody] CreateSubCategoryCommandRequest createSubCategoryCommandRequest)
        {
            await mediator.Send(createSubCategoryCommandRequest);
            return Ok();
        }


    }
}
