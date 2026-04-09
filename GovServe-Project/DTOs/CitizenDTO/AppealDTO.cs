using System.ComponentModel.DataAnnotations;
using GovServe_Project.Enum;

namespace GovServe_Project.DTOs
{
	public class AppealDTO
	{
	    public int UserId { get; set; }

		public int ApplicationID { get; set; }
		[Required]
		[StringLength(500)]
		public string? Reason { get; set; }

		public string? Description { get; set; }

		public DateTime FiledDate{  get; set; }

		public AppealStatus Status { get; set; }

	}
}
