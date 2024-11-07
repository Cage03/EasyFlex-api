using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class CategoryDal(dbo context) : ICategoryDal
{
    public async Task<int> CreateCategory(CategoryModel category)
    {
        bool alreadyExists = context.Categories.AnyAsync(model => model.Name.ToLower() == category.Name.ToLower() ).Result;
        if (!alreadyExists)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            int id = category.Id;
            return id;
        }
        else
        {
            return 0;
        }

    }

    public async Task<CategoryModel?> GetCategoryById(int id)
    {
        return await context.Categories.Include(c => c.Skills).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<CategoryModel>> GetCategories(int offset, int limit)
    {
        return await context.Categories
            .Skip(offset)
            .Take(limit)
            .Include(c => c.Skills)
            .ToListAsync(); 
    }
}