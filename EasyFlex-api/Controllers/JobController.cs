using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("api/Jobs")]
[ApiController]
public class JobController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IJobHandler _jobHandler = logicFactoryBuilder.BuildHandlerFactory().GetJobHandler();

    [HttpPost]
    [Route("Create")]
    public IActionResult CreateJob([FromBody] JobModel job)
    {
        try
        {
            _jobHandler.CreateJob(job);
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