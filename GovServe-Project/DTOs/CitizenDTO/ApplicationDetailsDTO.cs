namespace GovServe_Project.DTOs.CitizenDTO
{
	public class ApplicationDetailsDTO
	{
		// SECTION 1: Basic Application Info
		public int ApplicationId { get; set; }
		public string ServiceName { get; set; }
		public string DepartmentName { get; set; }
		public string ApplicationStatus { get; set; }
		public DateTime SubmittedDate { get; set; }

		// SECTION 2: Citizen Personal Details
		public CreateCitizenDetailsDTO CitizenInfo { get; set; }

		// SECTION 3: List of Documents
		public List<CitizenDocumentViewDTO> Documents { get; set; }
	}
}
