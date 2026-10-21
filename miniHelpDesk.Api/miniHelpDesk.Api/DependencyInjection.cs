using miniHelpDesk.Application;
using miniHelpDesk.Infrastructure;

namespace miniHelpDesk.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiDI(this IServiceCollection services)
        {
            services.AddApplicationDI()
                .AddInfrastructureDI();

            return services;
        }
    }
}
