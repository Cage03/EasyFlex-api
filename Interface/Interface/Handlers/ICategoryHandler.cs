using Interface.Dtos;
using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ICategoryHandler
{
    public Task CreateCategory(Category category);
    
    public Task<Category> GetCategoryById(int id);
    public Task<List<Category>> GetCategories(int pageNumber, int limit);
    public Task UpdateCategory(Category category);
    public Task DeleteCategory(int id);
}