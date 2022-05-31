using System.Collections.Generic;
using System.Threading.Tasks;
using FactoryApp.Dtos;
using FactoryApp.Services.Base;
using FactoryApp.Services.RawMaterials;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApp.Controllers
{
    [ApiController]
    [Route("api/raw-material")]
    public class RawMaterialController : ControllerBase
    {
        private readonly IRawMaterialsListService rawMaterialsListService;

        public RawMaterialController(IRawMaterialsListService rawMaterialsListService)
        {
            this.rawMaterialsListService = rawMaterialsListService;
        }

        [HttpGet("list")]
        public async Task<ActionResult<RawMaterialsListDto>> GetRawMaterialsList()
        {
            ServiceResult<IEnumerable<RawMaterialsListDto>> result = await rawMaterialsListService.GetRawMaterialsListAsync();

            if (result.Ok)
            {
                return Ok(result.Extras);
            }

            return Problem(result.Message);
        }
    }
}