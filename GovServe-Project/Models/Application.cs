using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GovServe_Project.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }

        public int UserId { get; set; }        // User FK

        public int ServiceId { get; set; }     // Service FK

        public string ServiceName { get; set; }

        public string Description { get; set; }

        public DateTime SubmittedDate { get; set; } = DateTime.Now;

        public string ApplicationStatus { get; set; } = "Submitted";

        public DateTime? CompletedDate { get; set; }

        //public virtual ICollection<CitizenDocument> CitizenDocuments { get; set; }
        //public virtual ICollection<Grievance> Grievances { get; set; }
    }
}