using Microsoft.AspNetCore.Mvc;
using ZealandIdScanner.Models;
using Microsoft.EntityFrameworkCore;
using ZealandIdScanner.EBbContext;

namespace ZealandIdScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly ZealandIdContext _context;

        public SensorsController(ZealandIdContext context)
        {
            _context = context;
        }

        [HttpPost("resetTable")]
        public IActionResult ResetSensorer()
        {
            try
            {
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Sensorer");
                _context.SaveChanges();

                //_context.Sensorer.AddRange(new List<Sensor> { new Sensor("ZA1", 1) });
                //_context.SaveChanges();

                return Ok("Sensorer table reset successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error resetting Sensorer table: {ex.Message}");
            }
        }

        // GET: api/Sensors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sensors>>> GetSensorer()
        {
            if (_context.Sensors == null)
            {
                return NotFound();
            }
            return await _context.Sensors.ToListAsync();
        }

        // GET: api/Sensors/5
        [HttpGet("Id/{id}")]
        public async Task<ActionResult<Sensors>> GetSensor(int id)
        {
            if (_context.Sensors == null)
            {
                return NotFound("DbContext can'be null");
            }
            var sensor = await _context.Sensors.FindAsync(id);

            if (sensor == null)
            {
                return NotFound("No Such sensor exists");
            }
            return Ok(sensor);
        }

        // PUT: api/Sensors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensors sensor)
        {
            if (id != sensor.SensorId)
            {
                return BadRequest();
            }

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
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

        // POST: api/Sensors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensors>> PostSensor(Sensors sensor)
        {
            try
            {
                sensor.Validate();
            }
            catch (Exception ex)
            {
                return StatusCode(422, ex.Message);
            }
            if (_context.Sensors == null)
            {
                return Problem("Entity set 'ZealandIdDbContext.Sensorer'  is null.");
            }
            var relatedEntity = await _context.Lokaler.FindAsync(sensor);

            if (relatedEntity == null)
            {
                return StatusCode(422, "Invalid LokaleId");
            }
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new { id = sensor.SensorId }, sensor);
        }

        // DELETE: api/Sensors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            if (_context.Sensors == null)
            {
                return NotFound();
            }
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return (_context.Sensors?.Any(e => e.SensorId == id)).GetValueOrDefault();
        }
    }
}
