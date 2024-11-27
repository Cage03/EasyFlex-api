using Interface.Dtos;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using System.Xml.Linq;

namespace Logic.Handlers;

public class CategoryHandler(ICategoryDal categoryDal) : ICategoryHandler
{
    public async Task CreateCategory(Category category)
    {
        await categoryDal.CreateCategory(ToModel(category));
    }

    public async Task<Category> GetCategoryById(int id)
    {
        CategoryModel model = await categoryDal.GetCategoryById(id);
        return ToDto(model);
    }

    public async Task<List<Category>> GetCategories(int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        List<CategoryModel> categories = await categoryDal.GetCategories(offset, limit);
        return categories.Select(c => ToDto(c)).ToList();
    }

    public async Task UpdateCategory(Category category)
    {
         await categoryDal.UpdateCategory(ToModel(category));
    }

    public static CategoryModel ToModel(Category category)
    {
        return new CategoryModel
        {
            Id = category.Id,
            Name = category.Name,
            Skills = category.Skills.Select(s => SkillHandler.ToModel(s)).ToList()
        };
    }

    public static Category ToDto(CategoryModel categoryModel)
    {
        return new Category()
        {
            Id = categoryModel.Id,
            Name = categoryModel.Name,
            Skills = categoryModel.Skills.Select(s => SkillHandler.ToDto(s)).ToList()
        };
    }

    public async Task DeleteCategory(int id)
    {
        if (id <= 0) throw new IndexOutOfRangeException();
        
        await categoryDal.DeleteCategory(id);
    }
}