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
    /// Defines the method to query all the user's requests.
    /// </summary>
    public interface IGetAllUserRequestsService
    {
        /// <summary>
        /// Allows to get all the user's requests.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <param name="status">The status desired.</param>
        /// <returns>User's requests.</returns>
        Task<ServiceResult<IEnumerable<UserRequestDto>>> GetAsync(int userId, RequestStatus? status = null);
    }

    /// <summary>
    /// Implements the method to query the user's requests.
    /// </summary>
    public class GetAllUserRequestsService : IGetAllUserRequestsService
    {
        private readonly ApplicationDbContext dbContext;

        public GetAllUserRequestsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<IEnumerable<UserRequestDto>>> GetAsync(int userId, RequestStatus? status = null)
        {
            var result = new ServiceResult<IEnumerable<UserRequestDto>>();

            IQueryable<Request> query = dbContext.Requests.Where(r => r.AuthorId == userId);

            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            var includes = new string[] { "Author", "Approver", "Details" };

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            result.Extras = await query.OrderByDescending(r => r.CreatedAt)
                .Select(r => new UserRequestDto()
                {
                    Approver = r.Approver.Name,
                    CreatedAt = r.CreatedAt,
                    Author = r.Author.Name,
                    Id = r.Id,
                    Status = r.Status,
                    TotalItems = r.Details.Count
                }).ToListAsync();

            result.Ok = true;

            return result;
        }
    }
}