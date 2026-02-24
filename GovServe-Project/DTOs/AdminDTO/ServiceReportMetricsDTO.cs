namespace GovServe_Project.DTOs.AdminDTO
{
    public class ServiceReportMetricsDTO
    {
        public int ApplicationsCount { get; set; }

        public double ApprovalRate { get; set; }

        public double AvgTurnaroundDays { get; set; }

        public double SLABreachRate { get; set; }
    }
}
