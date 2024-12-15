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
        public async Task<ActionResult<IrrigationScheduleDto>> PostIrrigationSchedule(IrrigationScheduleDto irrigationScheduleDto)
        {
            await _unitOfWork.IrrigationScheduleRepository.Insert(IrrigationScheduleMapper.ToModel(irrigationScheduleDto));

            await _unitOfWork.Save();

            return CreatedAtAction("GetIrrigationSchedule", new { id = irrigationScheduleDto.Id }, irrigationScheduleDto);
        }

        [HttpPost("batch")]
        public async Task<ActionResult> PostOrUpdateIrrigationSchedules([FromBody] List<IrrigationScheduleDto> irrigationScheduleDtos)
        {
            if (irrigationScheduleDtos == null || !irrigationScheduleDtos.Any())
            {
                return BadRequest("No irrigation schedules provided.");
            }

            foreach (var irrigationScheduleDto in irrigationScheduleDtos)
            {
                var existingSchedule = _unitOfWork.IrrigationScheduleRepository
                    .Specify(new IrrigationSchedulesSpecificationByDay_TankId_Mode(irrigationScheduleDto.Day, irrigationScheduleDto.TankId, IrrigationMode.Planned)).FirstOrDefault();                   

                if (existingSchedule != null)
                {               
                    existingSchedule.StartTime = irrigationScheduleDto.StartTime;
                    existingSchedule.EndTime = irrigationScheduleDto.EndTime;
                    existingSchedule.Duration = irrigationScheduleDto.Duration;
                    existingSchedule.IsActive = irrigationScheduleDto.IsActive;
                    existingSchedule.SetMinimumTankLevel(irrigationScheduleDto.MinimumTankLevel);
                    existingSchedule.Sprinklers = irrigationScheduleDto.Sprinklers;
                    existingSchedule.Mode = irrigationScheduleDto.Mode;

                    _unitOfWork.IrrigationScheduleRepository.Update(existingSchedule);
                }
                else
                {
                    // Insert new schedule
                    var newSchedule = IrrigationScheduleMapper.ToModel(irrigationScheduleDto);
                    await _unitOfWork.IrrigationScheduleRepository.Insert(newSchedule);
                }

                // Deactivate other schedules if the new one is active
                if (irrigationScheduleDto.IsActive)
                {
                    DeactivateOtherSchedules(irrigationScheduleDto.Day, irrigationScheduleDto.TankId, irrigationScheduleDto.Id);
                }
            }

            await _unitOfWork.Save();

            return Ok();
        }

        private void DeactivateOtherSchedules(DayOfWeek day, long tankId, long activeScheduleId)
        {
            var otherSchedules = _unitOfWork.IrrigationScheduleRepository
                .Specify(new IrrigationSchedulesSpecificationSearchByDay_TankId_activeScheduleId(day, tankId, activeScheduleId)).ToList();

            foreach (var schedule in otherSchedules)
            {
                schedule.IsActive = false;
                _unitOfWork.IrrigationScheduleRepository.Update(schedule);
            }
        }

        // DELETE: api/IrrigationSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIrrigationSchedule(long id)
        {
            var irrigationSchedule = await _unitOfWork.IrrigationScheduleRepository.GetByID(id);
            if (irrigationSchedule == null)
                return NotFound();

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
