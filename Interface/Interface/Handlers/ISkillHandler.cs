using Interface.Dtos;
using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ISkillHandler
{
    public Task<Skill> GetSkillFromId(int id);
    public Task<List<Skill>> GetSkillsFromCategory(int limit, int page, int categoryId);
    public Task CreateSkill(string name, int categoryId);
    public Task DeleteSkill(int id);
    public Task UpdateSkill(Skill skill);
}