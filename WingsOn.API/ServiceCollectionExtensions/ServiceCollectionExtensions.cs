using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using WingsOn.BL;
using WingsOn.BL.DI;
using WingsOn.Dal;
using WingsOn.Dal.DI;
using WingsOn.Domain;

namespace WingsOn.API.ServiceCollectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBLServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonSearchManager, PersonSearchManager>();
        }

        public static void AddDalServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Person>, PersonRepository>();
            services.AddScoped<IRepository<Booking>, BookingRepository>();
            services.AddScoped<IRepository<Flight>, FlightRepository>();
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "WingsOn API"
                });
            });
        }
    }
}
