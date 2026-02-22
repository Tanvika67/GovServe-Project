using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.AdminDTO
{
    public class SLARecordResponseDTO
    {
        public int SLARecordID { get; set; }
        public int CaseID { get; set; } = default!;
        public int StageID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public SLAStatus Status { get; set; }
    }
}
