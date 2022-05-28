using System;
using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;

namespace FactoryApp.Services
{
    public interface ICreateRequestService
    {
        Task<ServiceResult<int>> CreateRequestAsync(CreateRequestDto request);
    }

    public class CreateRequestService : ICreateRequestService
    {
        private readonly ApplicationDbContext dbContext;

        public CreateRequestService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ServiceResult<int>> CreateRequestAsync(CreateRequestDto request)
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