using Interface.Models;

namespace Interface.Interface.Dal;

public interface ISkillDal
{
    public Task<List<SkillModel>> GetSkills(List<int>? skillIds);
}