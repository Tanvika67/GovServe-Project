public class CaseDetailsDto
{
	// Case Info
	public int CaseId { get; set; }
	public int ApplicationID { get; set; }
	public string Status { get; set; }
	public int AssignedOfficerId { get; set; }
	public string OfficerName{  get; set; }
	public DateTime AssignedDate { get; set; }

	// Citizen Personal Details
	public string FullName { get; set; }
	public string Gender { get; set; }
	public DateTime DateOfBirth { get; set; }
	public string FatherName { get; set; }
	public string MotherName { get; set; }

	public string Email { get; set; }
	public string Phone { get; set; }

	public string AddressLine1 { get; set; }
	public string AddressLine2 { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Pincode { get; set; }

	public string AadhaarNumber { get; set; }

	// Documents
	public List<string> Documents { get; set; }
}
