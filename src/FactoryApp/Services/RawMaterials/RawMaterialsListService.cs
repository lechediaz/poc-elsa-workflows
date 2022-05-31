using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.RawMaterials
{
    /// <summary>
    /// Defines the method to get the users list.
    /// </summary>
    public interface IRawMaterialsListService
    {
        /// <summary>
        /// Allows to get the users list.
        /// </summary>
        /// <returns>Service result.</returns>
        Task<ServiceResult<IEnumerable<RawMaterialsListDto>>> GetRawMaterialsListAsync();
    }

    public class RawMaterialsListService : IRawMaterialsListService
    {
        private readonly ApplicationDbContext dbContext;

        public RawMaterialsListService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult<IEnumerable<RawMaterialsListDto>>> GetRawMaterialsListAsync()
        {
            var result = new ServiceResult<IEnumerable<RawMaterialsListDto>>();

            result.Extras = await dbContext.RawMaterials
                .OrderBy(r => r.Name)
                .Select(r => new RawMaterialsListDto()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                })
                .ToListAsync();

            result.Ok = true;

            return result;
        }
    }
}