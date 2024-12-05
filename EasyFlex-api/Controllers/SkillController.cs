using System.Text.Json;
using Interface.Dtos;
using Interface.Factories;
using Interface.Interface.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    
    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetSkillById(int id)
    {
        try
        {
            var skills = await _skillHandler.GetSkillFromId(id);
            return Ok(skills);
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateSkill([FromBody] JsonElement body)
    {
        try
        {
            string? name = body.GetProperty("name").GetString();
            int categoryId = body.GetProperty("categoryId").GetInt32();

            if (string.IsNullOrEmpty(name) || categoryId <= 0) throw new Exception("Missing values for creating a new skill");

            await _skillHandler.CreateSkill(name, categoryId);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteSkill([FromQuery] int id)
    {
        try
        {
            await _skillHandler.DeleteSkill(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateSkill([FromBody] Skill skill)
    {
        if (skill.Name.IsNullOrEmpty()) return StatusCode(400, "No name provided for skill");
        
        try
        {
            await _skillHandler.UpdateSkill(skill);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
}