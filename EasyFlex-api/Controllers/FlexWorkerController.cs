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
    
    private readonly ISkillHandler _skillHandler =logicFactoryBuilder.BuildHandlerFactory().GetSkillHandler();

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
    public async Task<IActionResult> GetFlexworkerById(int id)
    {
        try
        {
            return Ok(await _flexWorkerHandler.GetFlexworkerById(id));
        }
        catch (Exception ex)
        {
            return NotFound(ex);
        }
    }
    
    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteFlexWorker(int id)
    {
        try
        {
            await _flexWorkerHandler.DeleteFlexWorker(id);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    } 
    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateFlexWorker([FromBody] FlexworkerModel flexWorker)
    {
        try
        {
            await _flexWorkerHandler.UpdateFlexWorker(flexWorker);
            return Ok( );
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    [HttpPost]
    [Route("AddSkills")]
    public async Task<IActionResult> AddSkills(int flexWorkerId, List<int> skillIds)
    {
        try
        {
            List<SkillModel> skills = await _skillHandler.GetSkills(skillIds);
            await _flexWorkerHandler.AddSkills(flexWorkerId, skills);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }   
    
    [HttpDelete]
    [Route("DeleteSkills")]
    public async Task<IActionResult> DeleteSkills(int flexWorkerId, List<int> skillIds)
    {
        try
        {
            List<SkillModel> skills = await _skillHandler.GetSkills(skillIds);
            await _flexWorkerHandler.DeleteSkills(flexWorkerId, skills);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
}