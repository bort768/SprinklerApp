using DataBaseService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Dto;
using Model.Mapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GateApiSpirinklerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprinklerController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public SprinklerController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<SprinklerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SprinklerDto>>> GetSprinklers()
        {
            var sprinklers = await _unitOfWork.SprinklerRepository.GetAllAsync();
            var sprinklersDto = sprinklers.Select(SprinklerMapper.ToDto);
            return Ok(sprinklersDto);
        }

        // GET api/<SprinklerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SprinklerDto>> GetSprinklerById(long id)
        {
            var sprinkler = await _unitOfWork.SprinklerRepository.GetByID(id);

            if (sprinkler == null)
            {
                return NotFound();
            }

            return Ok(SprinklerMapper.ToDto(sprinkler));
        }

        // POST api/<SprinklerController>
        [HttpPost]
        public async Task<ActionResult<SprinklerDto>> PostSprinkler([FromBody] SprinklerDto sprinklerDto)
        {
            var sprinkler = SprinklerMapper.ToModel(sprinklerDto);
            await _unitOfWork.SprinklerRepository.Insert(sprinkler);
            await _unitOfWork.Save();

            return CreatedAtAction("GetSprinkler", new { id = sprinkler.Id }, sprinkler);
        }

        // PUT api/<SprinklerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSprinkler(long id, [FromBody] SprinklerDto sprinklerDto)
        {
            var sprinkler = SprinklerMapper.ToModel(sprinklerDto);

            if (id != sprinkler.Id)
            {
                return BadRequest();
            }

            _unitOfWork.SprinklerRepository.Update(sprinkler);
            try
            {
                await _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                var result = await SprinklerExists(id);
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

        // DELETE api/<SprinklerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSprinkler(long id)
        {
            var sprinkler = await _unitOfWork.SprinklerRepository.GetByID(id);
            if (sprinkler == null)
            {
                return NotFound();
            }
            _unitOfWork.SprinklerRepository.Delete(id);
            await _unitOfWork.Save();

            return NoContent();
        }

        private async Task<bool> SprinklerExists(long id)
        {
            var sprinkler = await _unitOfWork.SprinklerRepository.GetByID(id);
            return sprinkler != null;
        }
    }
}
