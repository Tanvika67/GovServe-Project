using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GovServe.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GovServe.Models
{
	public class Escalation
	{
		[Key]
		public int EscalationId { get; set; }
		[Required]
		[ForeignKey("Case")]
		public int CaseId {  get; set; }
		//public virtual Cases CaseId { get; set; }       //==> why error
		[Required]
		[RegularExpression("Citizen",ErrorMessage ="RaisedByType must be Citizen")]
		public int? RaisedByType { get; set; }
		[Required]
		[StringLength(200,MinimumLength =5)]
		public string Reason { get; set; }
		[Required]
		[RegularExpression("Pending|Resolved|",ErrorMessage ="Status must be Pending or Resolved")]
		public string Status { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }

	}
}
