using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Job")]
[ApiController]
public class JobController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IJobHandler _jobHandler = logicFactoryBuilder.BuildHandlerFactory().GetJobHandler();

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> CreateJob([FromBody] JobModel job)
    {
        try
        {
            if (await _jobHandler.CreateJob(job) == 0)
            { return StatusCode(400, "Failed to create job."); }
            else { return Ok(); }
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
            JobModel? job = await _jobHandler.GetJob(id);
            return Ok(job);
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
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpPost]
    [Route("Delete")]
    public async Task<IActionResult> DeleteJob([FromBody] int id)
    {
        try
        {
            JobModel? job = await _jobHandler.GetJob(id);

            await _jobHandler.DeleteJob(job);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
}
