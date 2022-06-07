using FactoryApp.Services.RawMaterials;
using FactoryApp.Services.Requests;
using FactoryApp.Services.Users;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Defines the method to register our services.
    /// </summary>
    public static class RegisterServices
    {
        /// <summary>
        /// Adds our services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The services.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Raw Materials
            services.AddScoped<IRawMaterialsListService, RawMaterialsListService>();
            #endregion

            #region Requests
            services.AddScoped<IApproveRequestService, ApproveRequestService>();
            services.AddScoped<ICompleteRequestService, CompleteRequestService>();
            services.AddScoped<ICreateRequestService, CreateRequestService>();
            services.AddScoped<IDeleteRequestService, DeleteRequestService>();
            services.AddScoped<IDispatchRequestService, DispatchRequestService>();
            services.AddScoped<IEditRequestService, EditRequestService>();
            services.AddScoped<IGetAllUserRequestsService, GetAllUserRequestsService>();
            services.AddScoped<INegotiateRequestService, NegotiateRequestService>();
            services.AddScoped<IPublishRequestService, PublishRequestService>();
            services.AddScoped<IRejectRequestService, RejectRequestService>();
            services.AddScoped<IRequestsToCompleteService, RequestsToCompleteService>();
            services.AddScoped<IValidateRequestExistService, ValidateRequestExistService>();
            services.AddScoped<IViewRequestByIdService, ViewRequestByIdService>();
            #endregion

            #region Users
            services.AddScoped<IUsersListService, UsersListService>();
            #endregion

            return services;
        }
    }
}