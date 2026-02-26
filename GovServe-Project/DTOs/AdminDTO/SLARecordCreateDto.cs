using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.AdminDTO
{
    public class SLARecordCreateDto
    {
        public int CaseID { get; set; } 
        public int StageID { get; set; }
        public DateTime StartDate { get; set; }
    }
}
