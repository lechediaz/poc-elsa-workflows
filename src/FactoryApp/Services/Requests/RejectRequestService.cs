using System;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to reject a request.
    /// </summary>
    public interface IRejectRequestService
    {
        /// <summary>
        /// Allows to reject a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> RejectAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to reject a request.
    /// </summary>
    public class RejectRequestService : IRejectRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public RejectRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> RejectAsync(int requestId)
        {
            var result = new ServiceResult();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request request = await dbContext.Requests.FindAsync(requestId);

            if (request.Status != RequestStatus.Published)
            {
                result.Message = "The request must be published first.";
                return result;
            }

            request.Status = RequestStatus.Rejected;
            request.RejectedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}