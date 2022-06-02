using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to delete a request.
    /// </summary>
    public interface IDeleteRequestService
    {
        /// <summary>
        /// Allows to delete a request.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> DeleteAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to delete a request.
    /// </summary>
    public class DeleteRequestService : IDeleteRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public DeleteRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> DeleteAsync(int requestId)
        {
            var result = new ServiceResult();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestId);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request actualRequest = await dbContext.Requests.FindAsync(requestId);

            if (actualRequest.Status != RequestStatus.Draft)
            {
                result.Message = "The request must be in draft status.";
                return result;
            }

            dbContext.Requests.Remove(actualRequest);

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}