using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Flexworker")]
[ApiController]
public class FlexWorkerController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IFlexWorkerHandler _flexWorkerHandler =
        logicFactoryBuilder.BuildHandlerFactory().GetFlexWorkerHandler();

    [HttpGet]
    [Route("Get")]
    public IActionResult GetFlexWorkers()
    {
        try
        {
            return Ok(_flexWorkerHandler.GetFlexWorkers());
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> RegisterFlexWorker([FromBody] FlexworkerModel flexWorker)
    {
        try
        {
           await _flexWorkerHandler.CreateFlexWorker(flexWorker);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpGet]
    [Route("GetById")]
    public IActionResult GetFlexworkerById(int id)
    {
        try
        {
            return Ok(_flexWorkerHandler.SelectFlexworkerById(id));
        }
        catch (Exception ex)
        {
            return NotFound();
        }
    }
}