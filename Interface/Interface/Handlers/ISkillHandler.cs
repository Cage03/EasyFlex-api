using Interface.Dtos;
using Interface.Models;

namespace Interface.Interface.Handlers;

public interface ISkillHandler
{
    public Task<List<Skill>> GetSkills(List<int> skillIds);
}