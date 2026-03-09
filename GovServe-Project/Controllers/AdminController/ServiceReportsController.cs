using GovServe_Project.DTOs.AdminDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ServiceReportsController : ControllerBase
{
    private readonly IServiceReportService _service;

    public ServiceReportsController(IServiceReportService service)
    {
        _service = service;
    }

    [HttpPost("generate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GenerateReport([FromBody] ReportFilterRequest request)
    {
        var result = await _service.GenerateReportAsync(request);
        return Ok(result);
    }
}
