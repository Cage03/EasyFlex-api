using System.Text.Json;
using Interface.Dtos;
using Interface.Exceptions;
using Interface.Factories;
using Interface.Interface.Handlers;
using Logic.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Flexworker")]
[ApiController]
public class FlexworkerController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly IFlexworkerHandler _flexworkerHandler =
        logicFactoryBuilder.BuildHandlerFactory().GetFlexworkerHandler();

    private readonly ISkillHandler _skillHandler = logicFactoryBuilder.BuildHandlerFactory().GetSkillHandler();

    private readonly IMatchingHandler _matchingHandler = logicFactoryBuilder.BuildHandlerFactory().GetMatchingHandler();

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
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
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
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
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
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost]
    [Route("Update")]
    public async Task<IActionResult> UpdateFlexworker([FromBody] Flexworker flexworker)
    {
        try
        {
            await _flexworkerHandler.UpdateFlexworker(flexworker);
            return Ok();
        }

        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
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

            List<Skill> skills = new List<Skill>();
            foreach (int skillId in skillIds)
            {
                skills.Add(await _skillHandler.GetSkillFromId(skillId));
            }
            await _flexworkerHandler.AddSkills((int)flexworkerId, skills);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
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

            if (skillIds == null || skillIds.Count == 0)
            {
                return BadRequest("No skills provided");
            }
            
            List<Skill> skills = new List<Skill>();
            foreach (int skillId in skillIds)
            {
                skills.Add(await _skillHandler.GetSkillFromId(skillId));
            }
            await _flexworkerHandler.RemoveSkills((int)flexworkerId, skills);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Route("Matches")]
    public async Task<IActionResult> GetMatches([FromQuery] int id)
    {
        try
        {
            List<JobResult> matches = await _matchingHandler.GetMatchesForFlexworker(id);
            return Ok(matches);
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
}