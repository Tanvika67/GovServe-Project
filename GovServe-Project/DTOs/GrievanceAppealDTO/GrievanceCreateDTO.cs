namespace GovServe_Project.DTOs
{
	public class GrievanceCreateDTO
	{
		// DTO used when citizen raises grievance
	
			public int ApplicationID { get; set; }
			public int CitizenID { get; set; }
			public string Reason { get; set; }
			public string Description { get; set; }
	}
	
}
