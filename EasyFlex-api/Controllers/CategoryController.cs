using Interface.Dtos;
using Interface.Factories;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Dtos;
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
            List<CategoryModel> categories = await _categoryHandler.GetCategories(pageNumber, limit);
            // The reason for this is to counter recursion error due to skill having categorie field in it
            // This just enforces it by making sure the new list contains new categories with skills that only contain id and name
            List<CategoryModel> reformattedCategories = categories.ConvertAll<CategoryModel>(input => new CategoryModel
            {
                Id = input.Id,
                Name = input.Name,
                Skills = input.Skills.ToList().ConvertAll(skill => new SkillModel{Id = skill.Id, Name = skill.Name})
            } );
            return Ok(reformattedCategories);
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
            CategoryModel? category = await _categoryHandler.GetCategoryById(id);
            if (category != null)
            {
                CategoryModel reformattedCategory = new CategoryModel()
                {
                    Id = category.Id,
                    Name = category?.Name,
                    Skills = category?.Skills.ToList().ConvertAll(skill => new SkillModel{Id = skill.Id, Name = skill.Name})
                };
                return Ok(reformattedCategory);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(400, ex);
        }
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryModel category)
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