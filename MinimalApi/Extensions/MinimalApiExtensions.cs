using Application.Abstractions;
using DataAccess.Repositories;
using Application;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddDbContext<SocialDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
        }

        public static void RegisterEndpointDefinitions(this WebApplication app) 
        {
            var endpointDefinition = typeof(Program).Assembly
                .GetTypes().Where(t => t.IsAssignableTo(typeof(IEndpointDefinition))
                && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance).Cast<IEndpointDefinition>();

            foreach(var endpoint in endpointDefinition)
            {
                endpoint.RegisterEndpoints(app);
            }
        }
    }
}
