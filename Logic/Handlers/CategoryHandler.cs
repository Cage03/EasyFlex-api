using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Dtos;

namespace Logic.Handlers;

public class CategoryHandler(ICategoryDal categoryDal) : ICategoryHandler
{
    public async Task<int> CreateCategory(Category category)
    {
        return await categoryDal.CreateCategory(category.ToModel());
    }

    public async Task<Category> GetCategoryById(int id)
    {
        CategoryModel model = await categoryDal.GetCategoryById(id);
        return new Category(model);
    }

    public async Task<List<Category>> GetCategories(int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        List<CategoryModel> categories = await categoryDal.GetCategories(offset, limit);
        return categories.Select(c => new Category(c)).ToList();
    }

    public async Task UpdateCategory(Category category)
    {
         await categoryDal.UpdateCategory(category.ToModel());
    }
}