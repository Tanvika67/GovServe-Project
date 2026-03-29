using GovServe_Project.DTOs.CitizenDTO;
namespace GovServe_Project.DTOs.SupervisorDTO
{
	public class CaseDetailsDto
	{
		public int CaseId { get; set; }

		public int ApplicationId { get; set; }

		public int AssignedOfficerId { get; set; }

		public string Status { get; set; }

		public DateTime AssignedDate { get; set; }

		public bool IsEscalated { get; set; }

		public List<UploadCitizenDocumentResponseDTO> Documents { get; set; }
	}

}
