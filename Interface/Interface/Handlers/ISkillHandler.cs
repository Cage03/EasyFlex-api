using Interface.Dto;
using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ISkillHandler
{
    public Task<List<SkillModel>> GetSkillsFromIds(List<int> skillIds);
    public Task<List<SkillModel>> GetSkillsFromCategory(int limit, int page, int categoryId);
    public Task CreateSkill(string name, int categoryId);
    public Task DeleteSkill(int id);
    public Task UpdateSkill(SkillDto skill);
}