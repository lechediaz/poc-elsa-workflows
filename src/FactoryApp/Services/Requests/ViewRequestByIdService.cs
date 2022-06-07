using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Models;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to get a request by Id.
    /// </summary>
    public interface IViewRequestByIdService
    {
        /// <summary>
        /// Allows to get a request by Id.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>The request.</returns>
        Task<ServiceResult<ViewRequestDto>> GetAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to get a request by Id.
    /// </summary>
    public class ViewRequestByIdService : IViewRequestByIdService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public ViewRequestByIdService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<ViewRequestDto>> GetAsync(int requestId)
        {
            var result = new ServiceResult<ViewRequestDto>();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            IQueryable<Request> query = dbContext.Requests.Where(r => r.Id == requestId);

            var includes = new string[] { "Author", "Approver", "Details.RawMaterial" };

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            Request request = await query.FirstOrDefaultAsync();

            result.Extras = new ViewRequestDto()
            {
                ApprovedAt = request.ApprovedAt,
                Author = new UserInViewRequestDto()
                {
                    Id = request.AuthorId,
                    Email = request.Author.Email,
                    Name = request.Author.Name
                },
                CompletedAt = request.CompletedAt,
                CreatedAt = request.CreatedAt,
                Details = request.Details.Select(d => new DetailInViewRequestDto()
                {
                    RawMaterialId = d.RawMaterialId,
                    Name = d.RawMaterial.Name,
                    Quantity = d.Quantity
                }),
                InNegociationAt = request.InNegociationAt,
                InShipmentAt = request.InShipmentAt,
                Id = request.Id,
                Approver = new UserInViewRequestDto()
                {
                    Id = request.ApproverId,
                    Email = request.Approver.Email,
                    Name = request.Approver.Name
                },
                RejectedAt = request.RejectedAt,
                Status = request.Status
            };

            result.Ok = true;

            return result;
        }
    }
}