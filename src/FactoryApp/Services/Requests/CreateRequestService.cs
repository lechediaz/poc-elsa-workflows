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
        /// <param name="request">The request.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult<int>> CreateAsync(CreateRequestDto request);
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
        public async Task<ServiceResult<int>> CreateAsync(CreateRequestDto request)
        {
            var result = new ServiceResult<int>();

            RequestDetail[] details = request.Details?.Select(d => new RequestDetail()
            {
                Quantity = d.Quantity,
                RawMaterialId = d.RawMaterialId
            }).ToArray();

            var newRequest = new Request()
            {
                CreatedAt = DateTime.UtcNow,
                CreatedById = request.CreatedById,
                Details = details,
                ReceiverId = request.ReceiverId,
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