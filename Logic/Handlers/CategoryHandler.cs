using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class CategoryHandler(ICategoryDal categoryDal) : ICategoryHandler
{
    public async Task<int> CreateCategory(CategoryModel? category)
    {
        return await categoryDal.CreateCategory(category);
    }

    public async Task<CategoryModel?> GetCategoryById(int id)
    {
        return await categoryDal.GetCategoryById(id);
    }

    public async Task<List<CategoryModel?>> GetCategories(int pageNumber, int limit)
    {
        var offset = (pageNumber - 1) * limit;
        List<CategoryModel?> categories = await categoryDal.GetCategories(offset, limit);
        
        return categories;
    }
}