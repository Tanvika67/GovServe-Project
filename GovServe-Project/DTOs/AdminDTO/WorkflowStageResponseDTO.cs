namespace GovServe_Project.DTOs
{
    public class WorkflowStageResponseDTO
    {
        public int StageID { get; set; }
        public int ServiceID { get; set; }
        public string ResponsibleRole { get; set; } = string.Empty;
        public int SequenceNumber { get; set; }
        public int SLA_Days { get; set; }
    }
}

