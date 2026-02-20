using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GovServe.Models
{
    public class ServiceReport
    {
        [Key]
        public int ReportID { get; set; }

        //Application
        public int TotalApplications { get; set; }
        public int ApprovedApplication { get; set; }

        public int RejectedApplication { get; set; }
        public int PendingApplication { get; set; }
        
        //cases
        public int TotalCases { get; set; }
        public int ActiveCases { get; set; }
        public int EscalateCases { get; set; }

        //SLA Recode
        public int TotalSLARecords { get; set; }
        public int SLABreachedCases { get; set; }

        //Grievance
        public int TotalGrievances { get; set; }
        public int ResolvedGrievances { get; set; }
        public int pendingGrievances { get; set; }

        public DateTime ReportDate { get; set; } = DateTime.Now;
    }
}
