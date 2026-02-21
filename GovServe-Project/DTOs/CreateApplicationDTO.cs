using System.ComponentModel.DataAnnotations;
namespace GovServe_Project.DTOs
{
    public class CreateApplicationDTO
    {
		[Required]
		public int ServiceID { get; set; }
	}
}
