using DataBaseService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Dto;
using Model.Mapper;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GateApiSpirinklerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TankController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public TankController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<TankController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TankDto>>> GetTank()
        {
            var tanks = await _unitOfWork.TankRepository.GetAllAsync();
            var tankDtos = tanks.Select(tank => TankMapper.ToDto(tank));
            return Ok(tankDtos);
        }

        // GET api/<TankController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TankDto>> GetTankById(long id)
        {
            var tank = await _unitOfWork.TankRepository.GetByID(id);

            if (tank == null)
            {
                return NotFound();
            }         

            return Ok(TankMapper.ToDto(tank));
        }

        // POST api/<TankController>
        [HttpPost]
        public async Task<ActionResult<TankDto>> PostTank([FromBody] TankDto tankDto)
        {
            var tank = TankMapper.ToModel(tankDto);

            await _unitOfWork.TankRepository.Insert(tank);
            await _unitOfWork.Save();

            return CreatedAtAction("GetTank", new { id = tank.Id }, tank);
        }

        // PUT api/<TankController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTank(long id, TankDto tankDto)
        {
            var tank = TankMapper.ToModel(tankDto);

            if (id != tank.Id)
            {
                return BadRequest();
            }

            _unitOfWork.TankRepository.Update(tank);
            try
            {
                await _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                var result = await TankExists(id);
                if (result is not true)
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

        // DELETE api/<TankController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTank(long id)
        {
            var tank = await _unitOfWork.TankRepository.GetByID(id);
            if (tank == null)
            {
                return NotFound();
            }

            _unitOfWork.TankRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private async Task<bool> TankExists(long id)
        {
            var tank = await _unitOfWork.TankRepository.GetByID(id);
            return tank != null;
        }
    }
}
