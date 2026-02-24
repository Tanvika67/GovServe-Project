
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Repository_Implementation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Service_Implementation;
using GovServe_Project.Repository.Repository_Implentation

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
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IEligibilityRuleRepository, EligibilityRuleSrvice>();
            services.AddScoped<IRequiredDocumentRepository, RequiredDocumentRepository>();
            services.AddScoped<IWorkflowStageRepository, WorkflowStageRepository>();
            services.AddScoped<ISLARecordRepository, SLARecordRepository>();
            services.AddScoped<IServiceReportRepository, ServiceReportRepository>();


            //Services
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IEligibilityRuleService, EligibilityRuleService>();
            services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();
            services.AddScoped<IWorkflowStageService, WorkflowStageService>();
            services.AddScoped<ISLARecordService, SLARecordService>();
            services.AddScoped<IServiceReportService, ServiceReportService>();


            return services;
        }
    }
}


