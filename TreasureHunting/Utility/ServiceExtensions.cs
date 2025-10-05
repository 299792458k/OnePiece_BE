using TreasureHunting.BL;
using TreasureHunting.DL;

namespace TreasureHunting.Utility;

public static class ServiceExtensions
{
    /// <summary>
    /// inject services by Lifetime Services
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Dependency injection
        services.AddScoped<IBLMap, BLMap>();
        services.AddScoped<IDLMap, DLMap>();

        return services;
    }
}
