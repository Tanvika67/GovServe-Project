namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class DashboardStatsDto
	{
		public int TotalCases { get; set; }
		public int PendingCases { get; set; }
		public int AssignedCases { get; set; }
		public int CompletedCases { get; set; }
		public int EscalatedCases { get; set; }
	}
}
