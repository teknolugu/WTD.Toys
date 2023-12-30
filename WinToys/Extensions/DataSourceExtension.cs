using kDg.FileBaseContext.Extensions;
using Microsoft.Extensions.DependencyInjection;
using WinToys.DataSource;

namespace WinToys.Extensions;

public static class DataSourceExtension
{
    public static IServiceCollection AddFileContext(this IServiceCollection services)
    {
        services.AddDbContext<EfDbContext>();

        return services;
    }
}