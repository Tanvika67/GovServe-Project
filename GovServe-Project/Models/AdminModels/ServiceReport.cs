
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("ServiceReports")]
public class ServiceReport
{
    [Key]
    public int ReportID { get; set; }

    [Required]
    public ReportScopeType Scope { get; set; } = default!;
    // Example: "Department-5", "Service-3", "Period-2025-01"

    [NotMapped]
    public ServiceReportMetricsDTO Metrics { get; set; } = new();

    public DateTime GeneratedDate { get; set; } = DateTime.UtcNow;
}