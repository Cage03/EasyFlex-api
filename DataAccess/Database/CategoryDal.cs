using Interface.Exceptions;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;
using Exception = System.Exception;

namespace DataAccess.Database;

public class CategoryDal(EasyFlexContext context) : ICategoryDal
{
    public async Task CreateCategory(CategoryModel category)
    {
        bool alreadyExists = context.Categories.AnyAsync(model => model.Name.ToLower() == category.Name.ToLower())
            .Result;
        if (!alreadyExists)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Category already exists");
        }
    }

    public async Task<CategoryModel> GetCategoryById(int id)
    {
        var category = await context.Categories.Include(c => c.Skills).FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
        {
            throw new NotFoundException("Category not found");
        }

        return category;
    }

    public async Task<List<CategoryModel>> GetCategories(int offset, int limit)
    {
        return await context.Categories
            .Skip(offset)
            .Take(limit)
            .Include(c => c.Skills)
            .ToListAsync();
    }

    public async Task UpdateCategory(CategoryModel category)
    {
        var oldCategory = context.Categories.FirstOrDefaultAsync(model => model.Id == category.Id).Result;
        bool alreadyExists = context.Categories
            .AnyAsync(model => model.Name.ToLower() == category.Name.ToLower() && model.Id != category.Id).Result;

        if (alreadyExists)
        {
            throw new Exception("Category already Exists");
        }

        if (oldCategory == null)
        {
            throw new NotFoundException("Category not found");
        }

        oldCategory.Name = category.Name;
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategory(int id)
    {
        var category = context.Categories.FirstOrDefaultAsync(model => model.Id == id);
        if (category.Result != null)
        {
            context.Categories.Remove(category.Result);
        }
        else
        {
            throw new NotFoundException("Category not found");
        }
        await context.SaveChangesAsync();
    }
}