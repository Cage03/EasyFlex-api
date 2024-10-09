using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Jobs")]
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
            await _jobHandler.CreateJob(job);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpGet]
    [Route("Get")]
    public IActionResult GetJob([FromQuery] int id)
    {
        try
        {
            JobModel? job = _jobHandler.GetJob(id).Result;
            return Ok(job);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpGet]
    [Route("GetJobs")]
    public async Task<IActionResult> GetJobs([FromQuery] int pageNumber)
    {
        try
        {
            var jobs = await _jobHandler.GetJobs(pageNumber);
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