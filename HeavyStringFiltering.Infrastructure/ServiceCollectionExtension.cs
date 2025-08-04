using HeavyStringFiltering.Application.Dtos;
using HeavyStringFiltering.Application.Services;
using HeavyStringFiltering.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HeavyStringFiltering.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureOptions(services, configuration);

            services.AddServices(configuration);
        }

        private static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IChunkStorageService, ChunkStorageService>();

            services.AddTransient<IFilteringService>(ctx =>
            {
                var appSettings = ctx.GetRequiredService<IOptions<AppSettings>>().Value;
                if (appSettings.FilterAlgorithm == LevenshteinFilteringService.FilterAlgorithm)
                {
                    return new LevenshteinFilteringService();
                }
                else if (appSettings.FilterAlgorithm == LevenshteinFilteringService.FilterAlgorithm)
                {
                    return new JaroWinklerFilteringService();
                }
                else
                    throw new Exception("No algoritm found for filtering logic in appsttings.json AppSettings->FilterAlgorithm");
            });

            services.AddTransient<IUploadService, UploadService>();
        }

        private static void ConfigureOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        }
    }
}