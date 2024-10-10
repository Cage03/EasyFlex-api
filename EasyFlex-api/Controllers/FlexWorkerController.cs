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
    public async Task<IActionResult> GetFlexWorkers(int limit, int page)
    {
        try
        {
            var flexWorkers = await _flexWorkerHandler.GetFlexWorkers(limit, page);
            return Ok(flexWorkers);
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
            return Ok(_flexWorkerHandler.GetFlexworkerById(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    [HttpPut]
    [Route("Update")]
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
    
    [HttpDelete]
    [Route("Delete")]
    public IActionResult DeleteFlexWorker(int id)
    {
        try
        {
            _flexWorkerHandler.DeleteFlexWorker(id);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}