
using GovServe_Project.Data;
using GovServe_Project.Repository.Interface;
using GovServe_Project.Repository.Interface.AdminRepositoryInterface;
using GovServe_Project.Repository.Repository_Implentation;
using GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation;
using GovServe_Project.Services.Interfaces;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using GovServe_Project.Services.Service_Implementation;
using GovServe_Project.Services.Service_Implementation.AdminServiceImplementation;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using GovServe_Project.Repository.Interface.SuperRepositoryInterface;
using GovServe_Project.Repository.Repository_Implentation.SuperRepositoryImplementation;
using GovServe_Project.Services.Interfaces.SuperServiceInterface;
using GovServe_Project.Services.Service_Implementation.SuperServiceImplementation;

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
            services.AddScoped<IEligibilityRuleRepository, EligibilityRuleRepository>();
            services.AddScoped<IRequiredDocumentRepository, RequiredDocumentRepository>();
            services.AddScoped<IWorkflowStageRepository, WorkflowStageRepository>();
            services.AddScoped<ISLARecordRepository, SLARecordRepository>();
            services.AddScoped<IServiceReportRepository, ServiceReportRepository>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<IEscalationRepository, EscalationRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISLADayRepository, SLADayRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGrievanceRepository, GrievanceRepository>();
            services.AddScoped<IAppealRepository, AppealRepository>();





            //Services
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IEligibilityRuleService, EligibilityRuleService>();
            services.AddScoped<IRequiredDocumentService, RequiredDocumentService>();
            services.AddScoped<IWorkflowStageService, WorkflowStageService>();
            services.AddScoped<ISLARecordService, SLARecordService>();
            services.AddScoped<IServiceReportService, ServiceReportService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IEscalationService, EscalationService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ISLADayService, SLADayService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGrievanceService, GrievanceService>();
            services.AddScoped<IAppealService, AppealService>();

            return services;




        }
    }
}