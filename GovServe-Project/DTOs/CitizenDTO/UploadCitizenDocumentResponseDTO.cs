namespace GovServe_Project.DTOs.CitizenDTO
{
    public class UploadCitizenDocumentResponseDTO
    {

		public int UserId { get; set; }

		public int CitizenDocumentID { get; set; }

		public int ApplicationID { get; set; }

		public string DocumentName { get; set; }

		public DateTime UploadedDate { get; set; }

		public IFormFile URI { get; set; }

		public string VerificationStatus { get; set; }
	}
}
