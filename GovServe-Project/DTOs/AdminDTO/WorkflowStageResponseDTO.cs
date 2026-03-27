using GovServe_Project.Enum;


namespace GovServe_Project.DTOs
{
        public class WorkflowStageResponseDto
        {
            public int StageID { get; set; }
            public string ServiceName { get; set; } = string.Empty;
            public string ResponsibleRole { get; set; } = string.Empty;
            public int SequenceNumber { get; set; }
            public int SLA_Days { get; set; }
        }
 }



