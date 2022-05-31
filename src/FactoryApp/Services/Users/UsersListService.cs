using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.Users
{
    /// <summary>
    /// Defines the method to get the users list.
    /// </summary>
    public interface IUsersListService
    {
        /// <summary>
        /// Allows to get the users list.
        /// </summary>
        /// <returns>Service result.</returns>
        Task<ServiceResult<IEnumerable<UsersListDto>>> GetUsersListAsync();
    }

    public class UsersListService : IUsersListService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersListService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<IEnumerable<UsersListDto>>> GetUsersListAsync()
        {
            var result = new ServiceResult<IEnumerable<UsersListDto>>();

            result.Extras = await dbContext.Users
                .Include("Supervisor")
                .OrderBy(u => u.Name)
                .Select(u => new UsersListDto()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = u.Name,
                    Role = u.Role,
                    Supervisor = u.Supervisor != null ? new UsersListDto()
                    {
                        Email = u.Supervisor.Email,
                        Id = u.Supervisor.Id,
                        Name = u.Supervisor.Name,
                        Role = u.Supervisor.Role,
                    } : null,
                    SupervisorId = u.SupervisorId
                })
                .ToListAsync();

            result.Ok = true;

            return result;
        }
    }
}