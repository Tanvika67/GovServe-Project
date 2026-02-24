using GovServe_Project.DTOs.AdminDTO;

public interface IServiceReportRepository
{
    Task<ServiceReportMetricsDTO> GenerateMetricsAsync(ReportFilterRequest request);
}
