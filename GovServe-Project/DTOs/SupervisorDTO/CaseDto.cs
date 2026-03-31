namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class CaseDto
	{
		public int caseId { get; set; }
		public int applicationID { get; set; }
		public string status { get; set; }
		public int? assignedOfficerId { get; set; }
		public string fullName { get; set; }
	}
}
