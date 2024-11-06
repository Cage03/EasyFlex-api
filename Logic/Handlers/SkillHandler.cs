using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class SkillHandler(ISkillDal skillDal) : ISkillHandler
{
    public async Task<List<SkillModel>> GetSkillsFromIds(List<int> skillIds)
    {
        var skills = await skillDal.GetSkills(skillIds);

        if (skills.Count != skillIds.Count)
        {
            throw new Exception("Not all skills found");
        }

        return skills;
    }


    public async Task<List<SkillModel>> GetSkillsFromCategory(int limit, int page, int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new Exception("Invalid category id");
        }

        return await skillDal.GetSkillsFromCategory(limit, page, categoryId);
    }


    public async Task CreateSkill(string name)
    {
        await skillDal.CreateSkill(name);
    }
}