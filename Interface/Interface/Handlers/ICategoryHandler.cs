using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ICategoryHandler
{
    public Task<int> CreateCategory(CategoryModel category);
}