namespace GovServe_Project.DTOs.CitizenDTO
{
	public class UploadMultipleDocumentsDTO
	{
	public int ApplicationID { get; set; }
	 public List<UploadCitizenDocumentDTO> Documents { get; set; }				
	}
}
