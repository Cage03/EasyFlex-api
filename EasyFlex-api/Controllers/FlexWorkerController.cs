using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[ApiController]
public class FlexWorkerController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IFlexWorkerHandler _flexWorkerHandler =
        logicFactoryBuilder.BuildHandlerFactory().GetFlexWorkerHandler();

    [HttpGet]
    [Route("/Get")]
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

    // [HttpGet]
    // [Route("/Get")]
    // public IActionResult GetFlexworkerById(int id)
    // {
    //     try
    //     {
    //         return Ok(_flexWorkerHandler.SelectFlexworkerById(id));
    //     }
    //     catch (Exception ex)
    //     {
    //         return NotFound();
    //     }
    // }

    [HttpPost]
    [Route("/Update")]
    public IActionResult UpdateFlexWorker([FromBody] FlexworkerModel flexWorker)
    {
        try
        {
            _flexWorkerHandler.UpdateFlexWorker(flexWorker);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}