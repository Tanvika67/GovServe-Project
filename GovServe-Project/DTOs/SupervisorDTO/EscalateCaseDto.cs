namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class EscalateCaseDto
	{
		public int CaseId { get; set; }
		public int NewOfficerId { get; set; }
		public int SupervisorId { get; set; }
		public string Reason { get; set; }
	}
}
