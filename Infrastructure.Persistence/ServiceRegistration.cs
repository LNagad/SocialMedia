using Core.Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region "InMemory Database"
            if (config.GetValue<bool>("InMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options
                .UseInMemoryDatabase("ApplicationDB"));
            } 
            else
            {
                services.AddDbContext<ApplicationContext>(options => options
                .UseSqlServer(config.GetConnectionString("DefaultConnection"), m => m
                .MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion


            #region "Repositories"
            
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            #endregion
        }
    }
}
