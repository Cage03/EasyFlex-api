using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ISkillHandler
{
    public Task<List<SkillModel>> GetSkills(List<int> skillIds);
}