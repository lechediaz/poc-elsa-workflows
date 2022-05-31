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
    /// Defines the method to approve a request.
    /// </summary>
    public interface IApproveRequestService
    {
        /// <summary>
        /// Allows to approve a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> ApproveAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to approve a request.
    /// </summary>
    public class ApproveRequestService : IApproveRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public ApproveRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> ApproveAsync(int requestId)
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

            if (request.Status != RequestStatus.Published)
            {
                result.Message = "The request must be published first.";
                return result;
            }

            request.Status = RequestStatus.Approved;
            request.ApprovedAt = DateTime.UtcNow;

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