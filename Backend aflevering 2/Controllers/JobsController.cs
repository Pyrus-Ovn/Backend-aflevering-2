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
    public class JobsController : ControllerBase
    {
        private readonly Backend_aflevering_2Context _context;

        public JobsController(Backend_aflevering_2Context context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            // get all jobs from db
            var dbJobs = await _context.Job.ToListAsync();

            dbJobs.ForEach(async j => await _context.Entry(j)
                .Collection(j => j.Models)
                .LoadAsync());


            

            return Ok(dbJobs.Adapt<JobDto>());
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<JobExpensesDto>> GetJob(long id)
        {
            var job = await _context.Job
               
                .Include(x=>x.Expenses)
                .FirstOrDefaultAsync(x=>x.JobId== id);

            if (job == null)
            {
                return NotFound();
            }

            return job.Adapt<JobExpensesDto>();
        }


       

        [HttpGet("Model/{modelId}")]


        public async Task<ActionResult<ModelJobsDto>> GetJobsByModel(long modelId)
        {

            var model = await _context.Model
                .Include(x => x.Jobs)
                .FirstOrDefaultAsync(x => x.ModelId == modelId);

            if (model == null)
            {
                return NotFound();
            }

            return model.Adapt<ModelJobsDto>();

        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(long id, UpdateJobDto jobDto)
        {
            if (id != jobDto.JobId)
            {
                return BadRequest();
            }

            var job = jobDto.Adapt<Job>();

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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


        [HttpPut("{id}/{modelId}")]
        public async Task<IActionResult> PutJob(long id, long modelId)
        {
            var job = await _context.Job
                .Include(x=>x.Models)
                .FirstOrDefaultAsync(x=>x.JobId== id);
            if (id != job?.JobId)
            {
                return BadRequest();
            }
            job.Models ??= new List<Model>();


            var model = await _context.Model.FindAsync(modelId);
            if (model == null)
            {
                return NotFound();
            }
            
            job.Models.Add(model);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<JobDto>> PostJob(JobDto jobDto)
        {
            
            var job = jobDto.Adapt<Job>();
            _context.Job.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.JobId }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(long id)
        {
            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Job.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //delete model on a job
        [HttpDelete("{id}/{modelId}")]
        public async Task<IActionResult> DeleteJob(long id, long modelId)
        {
            var job = await _context.Job
                .Include(x => x.Models)
                .FirstOrDefaultAsync(x => x.JobId == id);
            if (id != job?.JobId)
            {
                return BadRequest();
            }
            job.Models ??= new List<Model>();

            var model = await _context.Model.FindAsync(modelId);
            if (model == null)
            {
                return NotFound();
            }

            job.Models.Remove(model);


            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(long id)
        {
            return _context.Job.Any(e => e.JobId == id);
        }
    }
}
