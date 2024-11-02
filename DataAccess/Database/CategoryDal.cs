using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;

namespace DataAccess.Database;

public class CategoryDal(dbo context) : ICategoryDal
{
    public async Task<int> CreateCategory(CategoryModel category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        int id = category.Id; 
        return id;
    }
}