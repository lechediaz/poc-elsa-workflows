using System.Threading.Tasks;
using FactoryApp.Dtos;
using FactoryApp.Services;
using FactoryApp.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly ICreateRequestService createRequestService;

        public RequestController(ICreateRequestService createRequestService)
        {
            this.createRequestService = createRequestService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> Create(CreateRequestDto request)
        {
            ServiceResult<int> result = await createRequestService.CreateRequestAsync(request);

            if (result.Ok)
            {
                return Ok(result.Extras);
            }

            return Problem(result.Message);
        }
    }
}