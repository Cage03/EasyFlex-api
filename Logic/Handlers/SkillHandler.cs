using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Classes;

namespace Logic.Handlers;

public class SkillHandler(ISkillDal skillDal) : ISkillHandler
{
    public async Task<List<SkillModel>> GetSkillsFromIds(List<int> skillIds)
    {
        return await skillDal.GetSkills(skillIds);
    }

    public async Task<List<SkillModel>> GetSkillsFromCategory(int limit, int page, int categoryId)
    {
        return await skillDal.GetSkillsFromCategory(limit, page, categoryId);
    }
}