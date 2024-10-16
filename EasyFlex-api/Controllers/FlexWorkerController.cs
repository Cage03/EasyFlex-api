using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("api/Flexworkers")]
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
    [Route("/Register")]
    public IActionResult RegisterFlexWorker([FromBody] FlexworkerModel flexWorker)
    {
        try
        {
            _flexWorkerHandler.CreateFlexWorker(flexWorker);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}