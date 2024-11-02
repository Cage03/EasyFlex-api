using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class CategoryHandler(ICategoryDal categoryDal) : ICategoryHandler
{
    public async Task<int> CreateCategory(CategoryModel category)
    {
        return await categoryDal.CreateCategory(category);
    }
}