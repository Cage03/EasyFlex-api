using Interface.Models;

namespace Interface.Interface.Dal;

public interface ICategoryDal
{
    public Task<int> CreateCategory(CategoryModel category);
}