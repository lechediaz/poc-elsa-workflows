using System;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to dispatch a request.
    /// </summary>
    public interface IDispatchRequestService
    {
        /// <summary>
        /// Allows to dispatch a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> DispatchAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to dispatch a request.
    /// </summary>
    public class DispatchRequestService : IDispatchRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public DispatchRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> DispatchAsync(int requestId)
        {
            var result = new ServiceResult();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request request = await dbContext.Requests.FindAsync(requestId);

            if (request.Status != RequestStatus.InNegociation)
            {
                result.Message = "The request must be in negotiation status first.";
                return result;
            }

            request.Status = RequestStatus.InShipment;
            request.InShipmentAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}