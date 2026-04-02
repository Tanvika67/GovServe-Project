using GovServe_Project.Data;
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models.AdminModels;
using GovServe_Project.Models.CitizenModels;
using Microsoft.EntityFrameworkCore;

namespace GovServe_Project.Repository.Repository_Implentation.AdminRepositoryImplementation
{
    public class ServiceReportRepository : IServiceReportRepository
    {
        private readonly GovServe_ProjectContext _context;

        public ServiceReportRepository(GovServe_ProjectContext context)
        {
            _context = context;
        }

        public async Task<ServiceReportMetricsDTO> GenerateMetricsAsync(ReportFilterRequest request)
        {
            IQueryable<Application> applications = _context.Application
                .Include(a => a.Case);

            IQueryable<SLARecords> slaRecords = _context.SLARecords
                .Include(s => s.Case);

            // ✅ APPLY FILTERS
            switch (request.Scope)
            {
                case ReportScopeType.Department:
                    if (!request.DepartmentId.HasValue)
                        throw new ArgumentException("DepartmentId is required.");

                    applications = applications
                        .Where(a => a.DepartmentID == request.DepartmentId.Value);

                    slaRecords = slaRecords
                        .Where(s => s.Case.Application.DepartmentID == request.DepartmentId.Value);
                    break;

                case ReportScopeType.Service:
                    if (!request.ServiceId.HasValue)
                        throw new ArgumentException("ServiceId is required.");

                    applications = applications
                        .Where(a => a.ServiceID == request.ServiceId.Value);

                    slaRecords = slaRecords
                        .Where(s => s.Case.Application.ServiceID == request.ServiceId.Value);
                    break;

                case ReportScopeType.Period:
                    if (!request.StartDate.HasValue)
                        throw new ArgumentException("StartDate is required.");

                    applications = applications
                        .Where(a => a.SubmittedDate >= request.StartDate.Value);

                    slaRecords = slaRecords
                        .Where(s => s.StartDate >= request.StartDate.Value);
                    break;
            }

            var applicationList = await applications.ToListAsync();
            var slaList = await slaRecords.ToListAsync();

            int totalApplications = applicationList.Count;

            int approvedApplications = applicationList.Count(a =>
                a.ApplicationStatus == "Approved");

            int slaBreaches = slaList.Count(s =>
                s.Status == SLAStatus.Breached);

            double approvalRate = totalApplications == 0
                ? 0
                : (double)approvedApplications / totalApplications * 100;

            double avgTurnaroundDays = applicationList
            .Where(a =>a.CompletedDate.HasValue && a.CompletedDate.Value >= a.SubmittedDate)
           .Select(a => (a.CompletedDate.Value - a.SubmittedDate).TotalDays) .DefaultIfEmpty(0).Average();

            double slaBreachRate = totalApplications == 0
                ? 0
                : (double)slaBreaches / totalApplications * 100;

            return new ServiceReportMetricsDTO
            {
                ApplicationsCount = totalApplications,
                ApprovalRate = Math.Round(approvalRate, 2),
                AvgTurnaroundDays = Math.Round(avgTurnaroundDays, 2),
                SLABreachRate = Math.Round(slaBreachRate, 2)
            };
        }
    }
}
