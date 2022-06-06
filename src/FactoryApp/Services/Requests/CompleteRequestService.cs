using System;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to complete a request.
    /// </summary>
    public interface ICompleteRequestService
    {
        /// <summary>
        /// Allows to complete a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> CompleteAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to complete a request.
    /// </summary>
    public class CompleteRequestService : ICompleteRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public CompleteRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> CompleteAsync(int requestId)
        {
            var result = new ServiceResult();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request request = await dbContext.Requests
                .Include("Details.RawMaterial")
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request.Status != RequestStatus.InShipment)
            {
                result.Message = "The request must be in shipment status first.";
                return result;
            }

            request.Status = RequestStatus.Completed;
            request.CompletedAt = DateTime.UtcNow;

            // Update stocks.

            foreach (var item in request.Details)
            {
                item.RawMaterial.Stock = item.Quantity;
            }

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}