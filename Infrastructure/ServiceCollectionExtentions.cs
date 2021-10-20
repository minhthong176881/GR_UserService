using UserService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Infrastructure
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection UseSchemaPerTenant(this IServiceCollection services, IConfiguration configuration)
         => services.UseEFInterceptor<SchemaInterceptor>(configuration);

        private static IServiceCollection UseEFInterceptor<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, IInterceptor
        {
            return services
                .AddScoped<DbContextOptions>((serviceProvider) =>
                {
                    var tenant = serviceProvider.GetRequiredService<TenantInfo>();

                    var efServices = new ServiceCollection();
                    efServices.AddEntityFrameworkSqlServer();
                    efServices.AddScoped(s =>
                        serviceProvider.GetRequiredService<TenantInfo>()); // Allows DI for tenant info, set by parent pipeline via middleware
                    efServices.AddScoped<IInterceptor, T>(); // Adds the interceptor

                    var connectionString = configuration.GetConnectionString("DefaultConnection");

                    return new DbContextOptionsBuilder<UserDbContext>()
                        .UseInternalServiceProvider(efServices.BuildServiceProvider())
                        .UseSqlServer(connectionString)
                        .Options;
                })
                .AddScoped(s => new UserDbContext(s.GetRequiredService<DbContextOptions>()));
        }

        public static IServiceCollection UseConnectionPerTenant(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped((serviceProvider) =>
            {
                var tenant = serviceProvider.GetRequiredService<TenantInfo>(); // Get from parent service provider (ASP.NET MVC Pipeline)
                var connectionString = configuration.GetConnectionString(tenant.Name);
                var options = new DbContextOptionsBuilder<UserDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;
                var context = new UserDbContext(options);
                return context;
            });

            return services;
        }
    }
}