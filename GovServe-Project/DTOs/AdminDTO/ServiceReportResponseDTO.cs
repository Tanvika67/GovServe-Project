using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.AdminDTO
{
    public class ServiceReportResponseDTO
    {
        public int ReportID { get; set; }

        public string Scope { get; set; }

        public ServiceReportMetricsDTO Metrics { get; set; }

        public DateTime GeneratedDate { get; set; }
    }

}
