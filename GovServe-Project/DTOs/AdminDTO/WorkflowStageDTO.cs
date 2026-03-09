using GovServe_Project.Enum;

namespace GovServe_Project.DTOs.Admin
{
    public class WorkflowStageCreateDto
    {
        public int ServiceID { get; set; }
        public string ResponsibleRole { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
    }

}