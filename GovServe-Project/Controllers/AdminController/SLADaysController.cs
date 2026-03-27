

namespace GovServe_Project.Controllers.AdminController
{
    using GovServe_Project.DTOs.AdminDTO;
    using GovServe_Project.Services.Interfaces.AdminServiceInterface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class SLADaysController : ControllerBase
    {
        private readonly ISLADayService _service;

        public SLADaysController(ISLADayService service)
        {
            _service = service;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin,Supervisor,Officer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
      //  [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SLADayCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(Get),
                new { id = result.SLADayID },
                result);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, SLADayCreateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
