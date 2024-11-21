using Interface.Dtos;
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
            int id = await _categoryHandler.CreateCategory(category);
            if (id == 0)
            { return StatusCode(400,new { Message = "Failed to create category.", isDuplicate = true }); }
            else { return Ok(id); }
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
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
    public async Task<IActionResult> GetCategoryByID([FromQuery] int id)
    {
        try
        {
            Category category = await _categoryHandler.GetCategoryById(id);

            return Ok(category);    
        }
        catch (Exception ex)
        {
            if (ex.Message == "NotFound")
            {
                return NotFound();
            }
            else
            {
                return StatusCode(400, ex);
            }
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateCategory([FromBody] Category category)
    {
        try
        {
            await _categoryHandler.UpdateCategory(category);
            return Ok( );
        }
        catch (Exception ex)
        {
            bool alreadyExists = false;
            bool doesNotExist = false;
            bool isSameName = false;
            
            if (ex.Message == "alreadyExists") alreadyExists = true;
            else if (ex.Message == "doesNotExist") doesNotExist = true;
            else if (ex.Message == "isSameName") isSameName = true;
            else return StatusCode(400, ex);
            
            return StatusCode(400,new { message = ex.Message, alreadyExists = alreadyExists, doesNotExist = doesNotExist, isSameName = isSameName });
        }
    }
}