using System.Text.Json;
using Interface.Dtos;
using Interface.Factories;
using Interface.Interface.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Flexworker")]
[ApiController]
public class FlexworkerController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IFlexworkerHandler _flexworkerHandler =
        logicFactoryBuilder.BuildHandlerFactory().GetFlexworkerHandler();
    
    private readonly ISkillHandler _skillHandler =logicFactoryBuilder.BuildHandlerFactory().GetSkillHandler();

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> RegisterFlexworker([FromBody] Flexworker flexworker)
    {
        try
        {
            await _flexworkerHandler.CreateFlexworker(flexworker);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetFlexworkers(int limit, int page)
    {
        try
        {
            List<Flexworker> flexworkers = await _flexworkerHandler.GetFlexworkers(limit, page);
            return Ok(flexworkers);
        }
        catch (Exception e)
        {
            if (e.Message == "NotFound")
            { return NotFound(); }
            else
            { return BadRequest(e); }
        }
    }

    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetFlexworkerById(int id)
    {
        try
        {
            Flexworker flexworker = await _flexworkerHandler.GetFlexworkerById(id);
            return Ok(flexworker);
        }
        catch (Exception e)
        {
            if (e.Message == "NotFound")
            { return NotFound(); }
            else
            { return BadRequest(e); }
        }
    }
    
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteFlexworker(int id)
    {
        try
        {
            await _flexworkerHandler.DeleteFlexworker(id);
            return Ok();
        }
        catch (Exception e)
        {
            if (e.Message == "NotFound")
            { return NotFound(); }
            else
            { return BadRequest(e); }
        }
    } 
    
    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateFlexworker([FromBody] Flexworker flexworker)
    {
        try
        {
            await _flexworkerHandler.UpdateFlexworker(flexworker);
            return Ok( );
        }
        catch (Exception e)
        {
            if (e.Message == "NotFound")
            { return NotFound(); }
            else
            { return BadRequest(e); }
        }
    }
    
    [HttpPost]
    [Route("AddSkills")]
    public async Task<IActionResult> AddSkills(JsonElement body)
    {
        try
        {
            int? flexworkerId = body.GetProperty("flexWorkerId").GetInt32();
            List<int>? skillIds = JsonSerializer.Deserialize<List<int>>(body.GetProperty("skillIds").ToString());

            if (skillIds == null || skillIds.Count == 0)
            {
                return BadRequest("No skills provided");
            }

            List<Skill> skills = await _skillHandler.GetSkills(skillIds);
            await _flexworkerHandler.AddSkills((int)flexworkerId, skills);
            return Ok();
        }
        catch (Exception e)
        {
            if (e.Message == "NotFound")
            { return NotFound(); }
            else
            { return BadRequest(e); }
        }
    } 
    
    [HttpDelete]
    [Route("RemoveSkills")]
    public async Task<IActionResult> RemoveSkills(JsonElement body)
    {
        try
        {
            int? flexworkerId = body.GetProperty("flexWorkerId").GetInt32();
            List<int>? skillIds = JsonSerializer.Deserialize<List<int>>(body.GetProperty("skillIds").ToString());
            
            if(skillIds == null || skillIds.Count == 0)
            {
                return BadRequest("No skills provided");
            }

            List<Skill> skills = await _skillHandler.GetSkills(skillIds);
            await _flexworkerHandler.RemoveSkills((int)flexworkerId, skills);
            return Ok();
        }
        catch (Exception e)
        {
            if (e.Message == "NotFound")
            { return NotFound(); }
            else
            { return BadRequest(e); }
        }
    }
}