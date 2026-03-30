namespace GovServe_Project.DTOs.CitizenDTO
{
	public class UpdateCitizenDetailsDTO
	{
		public int PersonalDetailID { get; set; }
		public int ApplicationID { get; set; }
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
	}
}