using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend_aflevering_2.Data;
using Backend_aflevering_2.Models;
using Mapster;

namespace Backend_aflevering_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly Backend_aflevering_2Context _context;

        public ModelsController(Backend_aflevering_2Context context)
        {
            _context = context;
        }

        // GET: api/Models
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Model>>> GetModel()
        {
            var dbModel = await _context.Model.ToListAsync();
            return Ok(dbModel.Adapt<List<ModelDto>>());
        }

        // GET: api/Models/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SpecificModelDto>> GetModel(long id)
        {

            var model = await _context.Model
                .Include(x => x.Jobs)
                .Include(x => x.Expenses)
                .FirstOrDefaultAsync(x => x.ModelId == id);

            SpecificModelDto mDto = model.Adapt<SpecificModelDto>();



            if (model == null)
            {
                return NotFound();
            }

            return mDto;
        }

        // PUT: api/Models/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModel(long id, ModelDto modelDto)
        {

            if (id != modelDto.ModelId)
            {
                return BadRequest();
            }
          
            

            var model = modelDto.Adapt<Model>();

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
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

        // POST: api/Models
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Model>> PostModel(ModelDto modelDto)
        {
            var model = modelDto.Adapt<Model>();
            _context.Model.Add(model);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModel", new { id = model.ModelId }, model);
        }

        // DELETE: api/Models/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModel(long id)
        {

            var model = await _context.Model.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            _context.Model.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelExists(long id)
        {
            return _context.Model.Any(e => e.ModelId == id);
        }
    }
}
