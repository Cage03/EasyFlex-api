using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Classes;

namespace Logic.Handlers;

public class SkillHandler(ISkillDal skillDal) : ISkillHandler
{
    public async Task<List<SkillModel>> GetSkills(List<int> skillIds)
    {
        return await skillDal.GetSkills(skillIds);
    }
}