
using System.Threading.Tasks;
using global::GovServe_Project.DTOs.CitizenDTO;
using global::GovServe_Project.Services.Citizen;
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
		public async Task<IActionResult> Create(CreateCitizenDetailsDTO dto)
		{
			var result = await _service.CreatePersonalDetailsAsync(dto);
			return Ok(result);
		}

		//Update CitizenDetails record
		[HttpPut("update")]
		public async Task<IActionResult> Update(UpdateCitizenDetailsDTO dto)
		{
			var result = await _service.UpdatePersonalDetailsAsync(dto);
			return Ok(result);
		}

		//Get CitizenDetails by ApplicationId	
		[HttpGet("by-application/{applicationId}")]
		public async Task<IActionResult> GetByApplication(int applicationId)
		{
			var result = await _service.GetByApplicationIdAsync(applicationId);
			if (result == null) return NotFound();
			return Ok(result);
		}

		//Get CitizenDetails by PersonalDetailId
		[HttpGet("{personalDetailId}")]
		public async Task<IActionResult> GetById(int personalDetailId)
		{
			var result = await _service.GetByIdAsync(personalDetailId);
			if (result == null) return NotFound();
			return Ok(result);
		}
	}
 }

