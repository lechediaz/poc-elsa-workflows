using System;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to publish a request.
    /// </summary>
    public interface IPublishRequestService
    {
        /// <summary>
        /// Allows to publish a request.
        /// </summary>
        /// <param name="info">The publish info.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> PublishAsync(PublishRequestDto info);
    }

    /// <summary>
    /// Implements the method to publish a request.
    /// </summary>
    public class PublishRequestService : IPublishRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public PublishRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> PublishAsync(PublishRequestDto info)
        {
            var result = new ServiceResult();

            if (string.IsNullOrEmpty(info?.ApproveLink) || string.IsNullOrEmpty(info?.RejectLink))
            {
                result.Message = "Missing information.";
                return result;
            }

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(info.RequestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request request = await dbContext.Requests.FindAsync(info.RequestId);

            if (request.Status != RequestStatus.Draft)
            {
                result.Message = "The request must be in draft status.";
                return result;
            }

            request.Status = RequestStatus.Published;
            request.ApproveLink = info.ApproveLink;
            request.RejectLink = info.RejectLink;

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}