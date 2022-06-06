using System;
using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to create a request.
    /// </summary>
    public interface ICreateRequestService
    {
        /// <summary>
        /// Allows to create a request.
        /// </summary>
        /// <param name="requestCreation">The request.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult<int>> CreateAsync(CreateRequestDto requestCreation);
    }

    /// <summary>
    /// Implements the method to create a request.
    /// </summary>
    public class CreateRequestService : ICreateRequestService
    {
        private readonly ApplicationDbContext dbContext;

        public CreateRequestService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<int>> CreateAsync(CreateRequestDto requestCreation)
        {
            var result = new ServiceResult<int>();

            RequestDetail[] details = requestCreation.Details?.Select(d => new RequestDetail()
            {
                Quantity = d.Quantity,
                RawMaterialId = d.RawMaterialId
            }).ToArray();

            var newRequest = new Request()
            {
                CreatedAt = DateTime.UtcNow,
                AuthorId = requestCreation.AuthorId,
                Details = details,
                ApproverId = requestCreation.ApproverId,
                Status = RequestStatus.Draft
            };

            await dbContext.Requests.AddAsync(newRequest);
            await dbContext.SaveChangesAsync();

            result.Extras = newRequest.Id;
            result.Ok = true;

            return result;
        }
    }
}