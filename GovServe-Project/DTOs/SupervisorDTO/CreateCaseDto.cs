namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class CreateCaseDto
	{
		public int ApplicationId { get; set; }
		public int DepartmentId { get; set; }
		public int SupervisorId { get; set; } //who assigns
		public int AssignedOfficerId { get; set; } //who works
	}
}
