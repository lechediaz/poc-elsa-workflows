using FactoryApp.Services.Requests;

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
            services.AddScoped<IApproveRequestService, ApproveRequestService>();
            services.AddScoped<ICreateRequestService, CreateRequestService>();
            services.AddScoped<IPublishRequestService, PublishRequestService>();
            services.AddScoped<IRejectRequestService, RejectRequestService>();
            services.AddScoped<IValidateRequestExistService, ValidateRequestExistService>();

            return services;
        }
    }
}