using System.Text.Json.Serialization;
using BizFlow.Application.AuthServices;
using BizFlow.Application.Behavior;
using BizFlow.Application.Common;
using BizFlow.Application.FileServices;
using BizFlow.Application.MapperConfiguration;
using BizFlow.Application.Repositories.Base;
using BizFlow.Domain.Model.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BizFlow.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(config =>
        {
            var policy = new AuthorizationPolicyBuilder()
                             .RequireAuthenticatedUser()
                             .Build();
            config.Filters.Add(new AuthorizeFilter(policy));

        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }).AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        services.Scan(scan => scan.FromAssemblyOf<IApplication>()
       .AddClasses(classes => classes.AssignableTo<IApplication>())
       .AddClasses(x => x.AssignableTo(typeof(IBaseRepository<,,>)))
       .AsSelfWithInterfaces()
       .WithScopedLifetime());

        //services.AddValidatorsFromAssembly(typeof(IApplication).Assembly);

        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(IApplication).Assembly);
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            x.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        });

        services.AddAutoMapper(x => {
            x.AddMaps(typeof(IApplication).Assembly);

        });
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IFileService, FileService>();
        services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddAutoMapper(typeof(MappingProfile));

        //services.AddMemoryCache();
        //services.AddSingleton<ICacheService, MemoryCacheService>();
    }
}
