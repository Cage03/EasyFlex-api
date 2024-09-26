using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

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
}