namespace GovServe_Project.DTOs
{
	
		// DTO used when citizen files appeal
		public class AppealCreateDTO
		{
			public int GrievanceID { get; set; }
			public int CitizenID { get; set; }
			public string Reason { get; set; }
			public string Description { get; set; }
		}
	
}
