using System;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to negotiate a request.
    /// </summary>
    public interface INegotiateRequestService
    {
        /// <summary>
        /// Allows to negotiate a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> NegotiateAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to negotiate a request.
    /// </summary>
    public class NegotiateRequestService : INegotiateRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public NegotiateRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> NegotiateAsync(int requestId)
        {
            var result = new ServiceResult();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request request = await dbContext.Requests.FindAsync(requestId);

            if (request.Status != RequestStatus.Approved)
            {
                result.Message = "The request must be approved first.";
                return result;
            }

            request.Status = RequestStatus.InNegociation;
            request.InNegociationAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}