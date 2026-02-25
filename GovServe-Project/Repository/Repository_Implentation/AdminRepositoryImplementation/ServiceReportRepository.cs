using GovServe_Project.Data;
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Enum;
using GovServe_Project.Models;
using GovServe_Project.Models.AdminModels;
using Microsoft.EntityFrameworkCore;
using GovServe_Project.Models.CitizenModels;


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
            IQueryable<Application> Application = _context.Application;
            IQueryable<SLARecord> SLARecords = _context.SLARecords;


            // Apply Filters
            switch (request.Scope)
            {
                case ReportScopeType.Department:
                    if (!request.DepartmentId.HasValue)
                        throw new ArgumentException("DepartmentId is required.");
                    break;

                case ReportScopeType.Service:
                    if (!request.ServiceId.HasValue)
                        throw new ArgumentException("ServiceId is required.");
                    break;

                case ReportScopeType.Period:
                    if (!request.StartDate.HasValue)
                        throw new ArgumentException("StartDate is required.");
                    break;
            }

            var list = await Application.ToListAsync();

            int total = list.Count;
            int approved = list.Count(a => a.ApplicationStatus == "Approved");
            int slaBreached = list.Count(a => a.ApplicationStatus== "Breached");

            double approvalRate = total == 0 ? 0 :
                (double)approved / total * 100;

            //double avgTurnaround = list
            //    .Where(a => a.CompletedDate != null)
            //    .Select(a => (a.CompletedDate.Value - a.SubmittedDate).TotalDays)
            //    .DefaultIfEmpty(0)
            //    .Average();

            double slaRate = total == 0 ? 0 :
                (double)slaBreached / total * 100;

            return new ServiceReportMetricsDTO
            {
                ApplicationsCount = total,
                ApprovalRate = Math.Round(approvalRate, 2),
               // AvgTurnaroundDays = Math.Round(avgTurnaround, 2),
                SLABreachRate = Math.Round(slaRate, 2)
            };
        }
    }


}

