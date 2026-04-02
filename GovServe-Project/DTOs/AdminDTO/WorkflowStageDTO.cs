namespace GovServe_Project.DTOs
{
    public class WorkflowStageCreateDto
    {
        public int ServiceID { get; set; }
        public int RoleID { get; set; }   // ✅ FIXED
        public int SequenceNumber { get; set; }
        //SAL Days Automaticalluy fetch
    }
}