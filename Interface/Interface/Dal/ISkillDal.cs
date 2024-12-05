using Interface.Models;

namespace Interface.Interface.Dal;

public interface ISkillDal
{
    public Task<SkillModel> GetSkill(int id);
    public Task<List<SkillModel>> GetSkillsFromCategory(int limit, int page, int categoryId);
    public Task CreateSkill(string name, int categoryId);
    public Task DeleteSkill(int id);
    public Task UpdateSkill(SkillModel skill);
}