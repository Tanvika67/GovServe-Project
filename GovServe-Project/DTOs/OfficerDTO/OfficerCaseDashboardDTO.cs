namespace GovServe_Project.DTOs.OfficerDTO
{
    public class OfficerCaseDashboardDTO
    {
        public int CaseId { get; set; }
        public string ApplicationName { get; set; }
        public string Status { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int DaysRemaining { get; set; }

        public string UrgencyLevel => DaysRemaining < 0 ? "Breached" :
                                     DaysRemaining <= 2 ? "Critical" : "OnTime";
    }
}
