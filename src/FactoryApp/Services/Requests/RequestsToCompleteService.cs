using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Enums;
using FactoryApp.Models;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to query all pending requests to complete.
    /// </summary>
    public interface IRequestsToCompleteService
    {
        /// <summary>
        /// Allows to get all pending requests to complete.
        /// </summary>
        /// <returns>User's requests.</returns>
        Task<ServiceResult<IEnumerable<RequestPendingDto>>> GetAsync();
    }

    /// <summary>
    /// Implements the method to query pending requests to complete.
    /// </summary>
    public class RequestsToCompleteService : IRequestsToCompleteService
    {
        private readonly ApplicationDbContext dbContext;

        public RequestsToCompleteService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<IEnumerable<RequestPendingDto>>> GetAsync()
        {
            var result = new ServiceResult<IEnumerable<RequestPendingDto>>();

            IQueryable<Request> query = dbContext.Requests.Where(r => r.Status >= RequestStatus.Approved
                && r.Status != RequestStatus.Rejected && r.Status != RequestStatus.Completed);

            var includes = new string[] { "Author", "Approver", "Details" };

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            result.Extras = await query.OrderByDescending(r => r.CreatedAt)
                .Select(r => new RequestPendingDto()
                {
                    Approver = r.Approver.Name,
                    Author = r.Author.Name,
                    CreatedAt = r.CreatedAt,
                    Id = r.Id,
                    Status = r.Status,
                    TotalItems = r.Details.Count
                }).ToListAsync();

            result.Ok = true;

            return result;
        }
    }
}