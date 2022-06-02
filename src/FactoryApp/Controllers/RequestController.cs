using System.Collections.Generic;
using System.Threading.Tasks;
using FactoryApp.Dtos;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;
using FactoryApp.Services.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FactoryApp.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestController : ControllerBase
    {
        private readonly IApproveRequestService approveRequestService;
        private readonly ICreateRequestService createRequestService;
        private readonly IGetAllUserRequestsService getAllUserRequestsService;
        private readonly IViewRequestByIdService viewRequestByIdService;
        private readonly IPublishRequestService publishRequestService;
        private readonly IRejectRequestService rejectRequestService;

        public RequestController(
            IApproveRequestService approveRequestService,
            ICreateRequestService createRequestService,
            IGetAllUserRequestsService getAllUserRequestsService,
            IViewRequestByIdService viewRequestByIdService,
            IPublishRequestService publishRequestService,
            IRejectRequestService rejectRequestService)
        {
            this.approveRequestService = approveRequestService;
            this.createRequestService = createRequestService;
            this.getAllUserRequestsService = getAllUserRequestsService;
            this.viewRequestByIdService = viewRequestByIdService;
            this.publishRequestService = publishRequestService;
            this.rejectRequestService = rejectRequestService;
        }

        [HttpGet("all-from-user/{userId:int}")]
        public async Task<ActionResult<IEnumerable<UserRequestDto>>> GetAllFromuser(
            [FromRoute] int userId,
            [FromQuery] RequestStatus? status = null)
        {
            ServiceResult<IEnumerable<UserRequestDto>> result = await getAllUserRequestsService.GetAsync(userId, status);

            if (result.Ok)
            {
                return Ok(result.Extras);
            }

            return Problem(result.Message);
        }

        [HttpGet("{requestId:int}")]
        public async Task<ActionResult<ViewRequestDto>> GetById([FromRoute] int requestId)
        {
            ServiceResult<ViewRequestDto> result = await viewRequestByIdService.GetAsync(requestId);

            if (result.Ok)
            {
                return Ok(result.Extras);
            }

            return Problem(result.Message);
        }

        [HttpPost("create")]
        public async Task<ActionResult<int>> Create(CreateRequestDto request)
        {
            ServiceResult<int> result = await createRequestService.CreateAsync(request);

            if (result.Ok)
            {
                return Ok(result.Extras);
            }

            return Problem(result.Message);
        }

        [HttpPost("publish")]
        public async Task<ActionResult> Publish(PublishRequestDto info)
        {
            ServiceResult result = await publishRequestService.PublishAsync(info);

            if (result.Ok)
            {
                return Ok();
            }

            return Problem(result.Message);
        }

        [HttpPost("approve/{requestId:int}")]
        public async Task<ActionResult> Approve([FromRoute] int requestId)
        {
            ServiceResult result = await approveRequestService.ApproveAsync(requestId);

            if (result.Ok)
            {
                return Ok();
            }

            return Problem(result.Message);
        }

        [HttpPost("reject/{requestId:int}")]
        public async Task<ActionResult> Reject([FromRoute] int requestId)
        {
            ServiceResult result = await rejectRequestService.RejectAsync(requestId);

            if (result.Ok)
            {
                return Ok();
            }

            return Problem(result.Message);
        }
    }
}