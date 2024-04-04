using Microsoft.EntityFrameworkCore;
using Reactivities.Application.Activities;
using Reactivities.Application.Core;
using Reactivities.Persistence;

namespace ReactivitiesAPI.Extensions
{
    public static class ApplicationserviceExtenstion
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddLogging(loggingBuilder =>
            {
                // Configure logging options if needed
                loggingBuilder.AddConsole(); // Add console logging
                loggingBuilder.AddDebug(); // Add debug logging
                loggingBuilder.AddEventSourceLogger(); // Add event source logger
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHttpContextAccessor();
            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    //.AllowAnyOrigin() the origin needed has been added
                    .AllowAnyMethod()
                    .AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
           
            return services;
        }
    }
}
