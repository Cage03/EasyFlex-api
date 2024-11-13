using Interface.Models;

namespace Interface.Interface.Dal;

public interface ICategoryDal
{
    public Task<int> CreateCategory(CategoryModel category);
    
    public Task<CategoryModel?> GetCategoryById(int id);
    public Task<List<CategoryModel>> GetCategories(int offset, int limit);
    public Task UpdateCategory(CategoryModel category);
    public Task DeleteCategory(int id);
    
}