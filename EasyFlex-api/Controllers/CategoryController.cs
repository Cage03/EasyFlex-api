using Interface.Dtos;
using Interface.Exceptions;
using Interface.Factories;
using Interface.Interface.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EasyFlex_api.Controllers;

[Route("Category")]
[ApiController]
public class CategoryController(ILogicFactoryBuilder logicFactoryBuilder) : Controller
{
    private readonly ICategoryHandler _categoryHandler =
        logicFactoryBuilder.BuildHandlerFactory().GetCategoryHandler();

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateCategory([FromBody] Category category)
    {
        try
        {
            await _categoryHandler.CreateCategory(category);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpGet]
    [Route("GetCategories")]
    public async Task<IActionResult> GetCategories([FromQuery] int pageNumber, int limit)
    {
        try
        {
            List<Category> categories = await _categoryHandler.GetCategories(pageNumber, limit);
            return Ok(categories);
        }
        catch(Exception ex)
        {
            return StatusCode(400, ex);
        }

    }
    
    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetCategoryById([FromQuery] int id)
    {
        try
        {
            Category category = await _categoryHandler.GetCategoryById(id);

            return Ok(category);
        }
        catch (NotFoundException ex)
        {
            return NotFound();
        }
        catch (Exception ex)
        { 
            return StatusCode(400, ex);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateCategory([FromBody] Category category)
    {
        try
        {
            await _categoryHandler.UpdateCategory(category);
            return Ok();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex.Message);
        }
    }
}