using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FactoryApp.Data;
using FactoryApp.Dtos;
using FactoryApp.Models;
using FactoryApp.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace FactoryApp.Services.Requests
{
    /// <summary>
    /// Defines the method to edit a request.
    /// </summary>
    public interface IEditRequestService
    {
        /// <summary>
        /// Allows to edit a request.
        /// </summary>
        /// <param name="requestEdition">The request.</param>
        /// <returns>Service result.</returns>
        Task<ServiceResult> EditAsync(EditRequestDto requestEdition);
    }

    /// <summary>
    /// Implements the method to edit a request.
    /// </summary>
    public class EditRequestService : IEditRequestService
    {
        private readonly IValidateRequestExistService validateRequestExistService;
        private readonly ApplicationDbContext dbContext;

        public EditRequestService(
            IValidateRequestExistService validateRequestExistService,
            ApplicationDbContext dbContext)
        {
            this.validateRequestExistService = validateRequestExistService;
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<ServiceResult> EditAsync(EditRequestDto requestEdition)
        {
            var result = new ServiceResult();

            ServiceResult<bool> validationResult = await validateRequestExistService.ExistsAsync(requestEdition.Id);

            if (!validationResult.Ok)
            {
                result.Message = validationResult.Message;
                return result;
            }

            Request actualRequest = await dbContext.Requests.Include("Details")
                .FirstOrDefaultAsync(r => r.Id == requestEdition.Id);

            int[] rawMaterialsIdsInEdition = requestEdition.Details?.Select(d => d.RawMaterialId)
                .ToArray();

            int[] actualRawMaterialsIds = actualRequest.Details.Select(d => d.RawMaterialId)
                .ToArray();

            // Remove raw materials from actual details if they don't appear in the request edition.

            IEnumerable<RequestDetail> detailsToDelete = actualRequest.Details
                .Where(d => !rawMaterialsIdsInEdition.Contains(d.RawMaterialId));

            if (detailsToDelete.Count() > 0)
            {
                dbContext.RequestDetails.RemoveRange(detailsToDelete);
            }

            // Add the new raw material according to the request edition.

            IEnumerable<RequestDetail> detailsToAdd = requestEdition?.Details
                .Where(d => !actualRawMaterialsIds.Contains(d.RawMaterialId))
                .Select(d => new RequestDetail()
                {
                    Quantity = d.Quantity,
                    RawMaterialId = d.RawMaterialId,
                    RequestId = requestEdition.Id
                });

            if (detailsToAdd?.Count() > 0)
            {
                dbContext.RequestDetails.AddRange(detailsToAdd);
            }

            // Update actual raw materials in details if they were modified in request edition.

            IEnumerable<RequestDetail> detailsToUpdate = actualRequest.Details
                .Where(d => rawMaterialsIdsInEdition.Contains(d.RawMaterialId))
                .Select(d =>
                {
                    d.Quantity = requestEdition.Details
                        .First(rd => rd.RawMaterialId == d.RawMaterialId)
                        .Quantity;

                    return d;
                }).ToArray();

            await dbContext.SaveChangesAsync();

            result.Ok = true;

            return result;
        }
    }
}