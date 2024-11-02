using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
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
    public async Task<IActionResult> CreateCategory([FromBody] CategoryModel category)
    {
        try
        {
            int id = await _categoryHandler.CreateCategory(category);
            if (id == 0)
            { return StatusCode(400, "Failed to create category."); }
            else { return Ok(id); }
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }
}