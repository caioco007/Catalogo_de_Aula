using Data.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projeto_API.Data;
using Projeto_API.Data.Repositorio;
using Projeto_API.Interface.Repositorio;
using Projeto_API.Interface.Service;
using Projeto_API.Services;

namespace Projeto_API.Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ModuloContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("ModuloContext")));


            services.AddTransient<IAulaService, AulaService>();
            services.AddTransient<IAulaRepositorio, AulaRepositorio>();

            services.AddTransient<IModuloService, ModuloService>();
            services.AddTransient<IModuloRepositorio, ModuloRepositorio>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepositorio, UserRepositorio>();


        }

    }
}
