using Interface.Dtos;
using Interface.Exceptions;
using Interface.Factories;
using Interface.Interface.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Job")]
[ApiController]
public class JobController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IJobHandler _jobHandler = logicFactoryBuilder.BuildHandlerFactory().GetJobHandler();
    private readonly IMatchingHandler _matchingHandler = logicFactoryBuilder.BuildHandlerFactory().GetMatchingHandler();

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> CreateJob([FromBody] Job job)
    {
        try
        {
            //todo validate name and address are not empty
            int id = await _jobHandler.CreateJob(job);
            if (id == 0)
            { return StatusCode(400, "Failed to create job."); }
            else { return Ok(id); }
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetJob([FromQuery] int id)
    {
        try
        {
            Job job = await _jobHandler.GetJob(id);
            return Ok(job);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpGet]
    [Route("GetJobs")]
    public async Task<IActionResult> GetJobs([FromQuery] int pageNumber, int limit)
    {
        try
        {
            var jobs = await _jobHandler.GetJobs(pageNumber, limit);
            return Ok(jobs);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteJob([FromQuery] int id)
    {
        try
        {
            await _jobHandler.DeleteJob(id);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
    
    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateJob([FromBody] Job job)
    {
        try
        {
            //todo validate name and address are not empty
            await _jobHandler.UpdateJob(job);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
    
    [HttpGet]
    [Route("Matches")]
    public async Task<IActionResult> GetMatches([FromQuery] int jobId)
    {
        try
        {
            var matches = await _matchingHandler.GetMatchesForJob(jobId);
            return Ok(matches);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
}
