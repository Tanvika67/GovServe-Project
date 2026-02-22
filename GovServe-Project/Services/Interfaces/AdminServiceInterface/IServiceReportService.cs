using GovServe_Project.DTOs.AdminDTO;

public interface IServiceReportService
{
    Task<ServiceReportResponseDTO> GenerateReportAsync(ReportFilterRequest request);
}