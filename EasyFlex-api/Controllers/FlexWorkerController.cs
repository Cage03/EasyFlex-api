using Interface.Factories;
using Interface.Interface.Handlers;
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
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}