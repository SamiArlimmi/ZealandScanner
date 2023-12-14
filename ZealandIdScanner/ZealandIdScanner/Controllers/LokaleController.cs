using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ZealandIdScanner.Models.Sensors;
using ZealandIdScanner.Models;
using ZealandIdScanner;
using ZealandIdScanner.EBbContext;
using Microsoft.EntityFrameworkCore;

namespace ZealandIdScanner.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LokaleController : ControllerBase
    {
        private readonly ZealandIdContext _dbContext;

        public LokaleController(ZealandIdContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Lokale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lokaler>>> GetLokaler()
        {
            if (_dbContext.lokaler == null)
            {
                return NotFound();
            }
            return await _dbContext.lokaler.ToListAsync();
        }

        [HttpGet("Id/{id}")]
        public async Task<ActionResult<Lokaler>> GetLokale(int id)
        {
            if (_dbContext.lokaler == null)
            {
                return NotFound("DbContext can'be null");
            }
            var lokale = await _dbContext.lokaler.FindAsync(id);

            if (lokale == null)
            {
                return NotFound("No Such lokale exists");
            }
            return Ok(lokale);
        }



        // GET: LokaleController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: api/Lokale
        [HttpPost]
        public async Task<ActionResult<Lokaler>> PostLokale(Lokaler lokale)
        {
            if (lokale == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                _dbContext.lokaler.Add(lokale);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("GetLokale", new { id = lokale.SensorId }, lokale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        //// GET: LokaleController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: LokaleController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: LokaleController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: LokaleController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}


