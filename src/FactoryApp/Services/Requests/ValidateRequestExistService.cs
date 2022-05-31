using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to validate if a requests exists.
    /// </summary>
    public interface IValidateRequestExistService
    {
        /// <summary>
        /// Allows to validate if a requests exists by Id.
        /// </summary>
        /// <param name="requestId">The request Id.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult<bool>> ExistsAsync(int requestId);
    }

    /// <summary>
    /// Implements the method to validate if a requests exists.
    /// </summary>
    public class ValidateRequestExistService : IValidateRequestExistService
    {
        private readonly ApplicationDbContext dbContext;

        public ValidateRequestExistService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<bool>> ExistsAsync(int requestId)
        {
            var result = new ServiceResult<bool>();

            result.Extras = await dbContext.Requests.AnyAsync(r => r.Id == requestId);

            if (!result.Extras)
            {
                result.Message = "Request not found.";
            }

            result.Ok = true;

            return result;
        }
    }
}