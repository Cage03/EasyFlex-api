using Interface.Exceptions;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class CategoryDal(EasyFlexContext context) : ICategoryDal
{
    public async Task<int> CreateCategory(CategoryModel category)
    {
        bool alreadyExists = context.Categories.AnyAsync(model => model.Name.ToLower() == category.Name.ToLower())
            .Result;
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
            throw new Exception("alreadyExists");
        }

        if (oldCategory != null)
        {
            // Check if the new name only changes in case or if it's unique
            if (oldCategory.Name.ToLower() == category.Name.ToLower() && oldCategory.Name == category.Name)
            {
                throw new Exception("isSameName");
            }

            oldCategory.Name = category.Name;
            await context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("doesNotExist");
        }
    }
}