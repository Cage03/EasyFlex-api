using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class SkillDal(EasyFlexContext context) : ISkillDal
{
    public async Task<List<SkillModel>> GetSkills(List<int>? skillIds)
    {
        List<SkillModel> skills = await context.Skills.Where(skill => skillIds.Contains(skill.Id)).ToListAsync();
        if (skills.Count != skillIds.Count) throw new Exception("Not all skills found");
        return skills;
    }
}