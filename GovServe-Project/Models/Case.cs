using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
namespace GovServe.Models
{
    public class Case
    {
        [Key]
        public int CaseId {  get; set; }   //Primary key
        [ForeignKey("Application")]
        public int ApplicationId{ get; set; } //FK from Application Table
		//public Application Application { get; set; }
		[Required]
		public int AssignedOfficerId {  get; set; }
		public string Status { get; set; }
		public string CurrentStage { get; set; }
		public DateTime AssignedDate { get; set; }
		public DateTime LastUpdated { get; set; }
        
        
         
    }
    
}
