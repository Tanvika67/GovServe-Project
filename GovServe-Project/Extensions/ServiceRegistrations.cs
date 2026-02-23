using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Repository_Implentation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Microsoft.EntityFrameworkCore.SqlServer.Update.Internal;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using GovServe_Project.Services.Service_Implementation.AdminServiceImplementation;
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
              // services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			   services.AddScoped<ISupervisorRepository, SupervisorRepository>();


			//Services
			services.AddScoped<IDepartmentService, DepartmentService>();
			services.AddScoped<ISupervisorService, SupervisorService>();



			return services;
            }
        }
 }


