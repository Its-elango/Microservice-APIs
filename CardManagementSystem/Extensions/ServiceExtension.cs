using CardManagementSystem.Repository;
using CardManagementSystem.Repository.Interface;
using CardManagementSystem.Service;

namespace CardManagementSystem.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICardTransaction,CardTransactionRepository>();
            services.AddTransient<Connection>();
            return services;
        }

        public static IServiceCollection RegisterClientService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<InvokeApi>();
            services.AddTransient<EmailService>();

            return services;
        }
    }
}
