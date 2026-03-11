
using GovServe_Project.DTOs.AdminDTO;
using GovServe_Project.Services.Interfaces.AdminServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GovServe_Project.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SLARecordsController : ControllerBase
    {
        private readonly ISLARecordService _service;

        public SLARecordsController(ISLARecordService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetAll()
        {
           return Ok(await _service.GetAllAsync());

        }
            

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> Get(int id)
        {
           return Ok(await _service.GetByIdAsync(id));

        }
           

        [HttpGet("breached")]
      //  [Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetBreachedCases()
        {
            var result = await _service.GetBreachedCasesAsync();
            return Ok(result);
        }

        [HttpGet("ontime")]
       // [Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetOnTimeCases()
        {
            var result = await _service.GetOnTimeCasesAsync();
            return Ok(result);
        }

        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SLARecordCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get),
                new { id = result.SLARecordID },
                result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }


}
