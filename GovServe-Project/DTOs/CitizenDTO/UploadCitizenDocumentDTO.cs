namespace GovServe_Project.DTOs.CitizenDTO
{ 
	public class UploadCitizenDocumentDTO
	{
	    public int UserId { get; set; }
		public int ApplicationID { get; set; }
		public int DocumentID{ get; set; }
		public IFormFile URI { get; set; }
	}
}
