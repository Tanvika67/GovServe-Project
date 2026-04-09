
using System.Threading.Tasks;
using global::GovServe_Project.DTOs.CitizenDTO;
using global::GovServe_Project.Services.Citizen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace GovServe_Project.Controllers.CitizenController
{
	[Route("api/[controller]")]
	[ApiController]

	public class CitizenDetailsController : ControllerBase
	{
		private readonly ICitizenDetailsService _service;

		public CitizenDetailsController(ICitizenDetailsService service)
		{
			_service = service;
		}


		//Create CitizenDetails record
		[HttpPost("create")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> Create(CreateCitizenDetailsDTO dto)
		{
			var result = await _service.CreatePersonalDetailsAsync(dto);
			return Ok(result);
		}

		[HttpPut("update-by-application/{applicationId}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> UpdateByAppId(int applicationId, [FromBody] UpdateCitizenDetailsDTO dto)
		{
			var result = await _service.UpdateByApplicationIdAsync(applicationId, dto);
			return Ok(new { Message = "Citizen details updated successfully", Data = result });
		}

		//Get CitizenDetails by ApplicationId	
		[HttpGet("by-application/{applicationId}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> GetByApplication(int applicationId)
		{
			var result = await _service.GetByApplicationIdAsync(applicationId);
			if (result == null) return NotFound();
			return Ok(result);
		}

		//Get CitizenDetails by PersonalDetailId
		[HttpGet("{personalDetailId}")]
		[Authorize(Roles = "Citizen")]
		public async Task<IActionResult> GetById(int personalDetailId)
		{
			var result = await _service.GetByIdAsync(personalDetailId);
			if (result == null) return NotFound();
			return Ok(result);
		}
	}
 }

