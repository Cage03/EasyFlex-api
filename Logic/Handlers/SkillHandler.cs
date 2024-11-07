using Interface.Dto;
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


    public async Task CreateSkill(string name, int categoryId)
    {
        await skillDal.CreateSkill(name, categoryId);
    }

    public async Task DeleteSkill(int id)
    {
        if (id <= 0) throw new IndexOutOfRangeException();
    
        await skillDal.DeleteSkill(id);
    }

    public async Task UpdateSkill(SkillDto skill)
    {
        if (skill.Id <= 0) throw new Exception("Invalid or no skill id provided");

        await skillDal.UpdateSkill(skill);
    }
}