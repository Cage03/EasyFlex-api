using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Skill")]
[ApiController]
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

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateSkill([FromBody]string name) //todo rework to dto object when categories are implemented
    {
        try
        {
            await _skillHandler.CreateSkill(name);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}