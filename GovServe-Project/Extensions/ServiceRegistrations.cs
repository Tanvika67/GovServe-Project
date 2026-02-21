


using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Repository_Implementation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
namespace GovServe_Project.Extensions
{
        public static class ServiceRegistration
        {
            public static IServiceCollection AddApplicationServices(
                this IServiceCollection services,
                IConfiguration configuration)
            {

         

                services.AddDbContext<GovServe_ProjectContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("GovServe_ProjectContext") ?? 
               throw new InvalidOperationException("Connection string 'GovServe_ProjectContext' not found.")));
               
              //Repository
               services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            //Services
            services.AddScoped<IDepartmentService, DepartmentService>();



            return services;
            }
        }
 }


