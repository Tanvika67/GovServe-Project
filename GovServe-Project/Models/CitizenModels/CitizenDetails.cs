using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Text.Json.Serialization;

	namespace GovServe_Project.Models.CitizenModels
	{
		public class CitizenDetails
		{
			[Key]
			public int PersonalDetailID { get; set; }

			[Required]
			public int ApplicationID { get; set; }

			[ForeignKey("ApplicationID")]
			[JsonIgnore]
			public virtual Application Application { get; set; }

			[Required]
			public string FullName { get; set; }

			[Required]
			public string Gender { get; set; }

			[Required]
			public DateTime DateOfBirth { get; set; }

			public string FatherName { get; set; }
			public string MotherName { get; set; }

			[Required, EmailAddress]
			public string Email { get; set; }

			[Required]
			public string Phone { get; set; }

			[Required]
			public string AddressLine1 { get; set; }
			public string AddressLine2 { get; set; }

			[Required]
			public string City { get; set; }

			[Required]
			public string State { get; set; }

			[Required]
			public string Pincode { get; set; }

			public string AadhaarNumber { get; set; }

		}
	}
