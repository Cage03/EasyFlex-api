using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ICategoryHandler
{
    public Task<int> CreateCategory(Category category);
    
    public Task<CategoryModel?> GetCategoryById(int id);
    public Task<List<CategoryModel>> GetCategories(int pageNumber, int limit);
    public Task UpdateCategory(CategoryModel category);
}