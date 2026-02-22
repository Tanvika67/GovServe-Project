using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.AdminDTO
{
    public class ReportFilterRequest
    {
        public ReportScopeType Scope { get; set; }

        public int? DepartmentId { get; set; }

        public int? ServiceId { get; set; }

        public DateTime? StartDate { get; set; }
    }

}
