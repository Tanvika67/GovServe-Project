namespace GovServe_Project.DTOs.CitizenDTO
{ 
	public class UploadCitizenDocumentDTO
	{
		public int ApplicationID { get; set; }

		public string DocumentName { get; set; }

		public IFormFile URI { get; set; }
	}
}
