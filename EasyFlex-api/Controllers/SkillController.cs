using Interface.Factories;
using Interface.Interface.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

public class SkillController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly ISkillHandler _skillHandler = logicFactoryBuilder.BuildHandlerFactory().GetSkillHandler();
    
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetSkills(int limit, int page, int categoryId)
    {
        try
        {
            var skills = await _skillHandler.GetSkillsFromCategory(limit, page, categoryId);
            return Ok(skills);
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}