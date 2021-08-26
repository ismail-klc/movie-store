using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using FluentValidation;
using Business.Validations;
using Entities.Concrete;
using Entities.Dtos;
using Data.Concrete;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Business.Helpers.Jwt;
using Business.Helpers.Logging;

namespace API.Helpers
{
    public static class StartupDependencies
    {
        public static IServiceCollection AddCloudscribeCore(this IServiceCollection services, IConfiguration configuration)
        {
            // injects
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IDirectorService, DirectorService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ILogger, ConsoleLogger>();

            services.AddAutoMapper(typeof(Startup));

            // swagger
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieStore", Version = "v1" });
                });

            // DB
            services.AddDbContext<MovieContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSql"),
                b => b.MigrationsAssembly("API"))
            );

            // validators
            services.AddTransient<IValidator<CreateDirectorDto>, DirectorValidator>();
            services.AddTransient<IValidator<CreateAdminDto>, AdminValidator>();
            services.AddTransient<IValidator<CreateActorDto>, ActorValidator>();
            services.AddTransient<IValidator<LoginDto>, LoginValidator>();
            services.AddTransient<IValidator<CreateCustomerDto>, CustomerValidator>();
            services.AddTransient<IValidator<CreateGenreDto>, GenreValidator>();
            services.AddTransient<IValidator<CreateMovieDto>, MovieValidator>();

            return services;
        }
    }
}