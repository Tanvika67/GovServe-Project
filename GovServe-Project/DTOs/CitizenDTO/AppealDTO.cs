using System.ComponentModel.DataAnnotations;

namespace GovServe_Project.DTOs
{
	public class AppealDTO
	{
		public int ApplicationID { get; set; }

		[Required]
		[StringLength(500)]
		public string Reason { get; set; }

		public string Description { get; set; }
		public string Remarks { get; set; } = "null";
	}
}
