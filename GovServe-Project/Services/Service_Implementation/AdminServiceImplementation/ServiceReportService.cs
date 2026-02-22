using GovServe_Project.DTOs.AdminDTO;

public class ServiceReportService : IServiceReportService
{
    private readonly IServiceReportRepository _repository;

    public ServiceReportService(IServiceReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceReportResponseDTO> GenerateReportAsync(ReportFilterRequest request)
    {
        var metrics = await _repository.GenerateMetricsAsync(request);

        return new ServiceReportResponseDTO
        {
            ReportID = new Random().Next(1000, 9999),
            Scope = request.Scope.ToString(),
            Metrics = metrics,
            GeneratedDate = DateTime.UtcNow
        };
    }
}