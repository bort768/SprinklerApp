using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using DataBaseService.Infrastructure;
using Model.Dto;
using Model.Mapper;

namespace GateApiSpirinklerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IrrigationSchedulesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public IrrigationSchedulesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/IrrigationSchedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IrrigationScheduleDto>>> GetIrrigationSchedules()
        {
            var irifationScheduleDto = await _unitOfWork.IrrigationScheduleRepository.GetAllAsync();
            return Ok(irifationScheduleDto.Select(IrrigationScheduleMapper.ToDto));
        }

        // GET: api/IrrigationSchedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IrrigationScheduleDto>> GetIrrigationSchedule(long id)
        {
            var irrigationSchedule = await _unitOfWork.IrrigationScheduleRepository.GetByID(id);

            if (irrigationSchedule == null)
            {
                return NotFound();
            }

            return IrrigationScheduleMapper.ToDto(irrigationSchedule);
        }

        // PUT: api/IrrigationSchedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIrrigationSchedule(long id, IrrigationSchedule irrigationSchedule)
        {
            if (id != irrigationSchedule.Id)
            {
                return BadRequest();
            }

            _unitOfWork.IrrigationScheduleRepository.Update(irrigationSchedule);
            //_context.Entry(irrigationSchedule).State = EntityState.Modified;

            try
            {
                await _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IrrigationScheduleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/IrrigationSchedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IrrigationSchedule>> PostIrrigationSchedule(IrrigationSchedule irrigationSchedule)
        {
            await _unitOfWork.IrrigationScheduleRepository.Insert(irrigationSchedule);

            await _unitOfWork.Save();

            return CreatedAtAction("GetIrrigationSchedule", new { id = irrigationSchedule.Id }, irrigationSchedule);
        }

        // DELETE: api/IrrigationSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIrrigationSchedule(long id)
        {
            var irrigationSchedule = await _unitOfWork.IrrigationScheduleRepository.GetByID(id);
            if (irrigationSchedule == null)
            {
                return NotFound();
            }

            _unitOfWork.IrrigationScheduleRepository.Delete(irrigationSchedule);
            await _unitOfWork.Save();

            return NoContent();
        }

        private bool IrrigationScheduleExists(long id)
        {
            var result = _unitOfWork.IrrigationScheduleRepository.GetByID(id);
            return result != null;
        }
    }
}
