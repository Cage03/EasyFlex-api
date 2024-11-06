using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class SkillDal(dbo context) : ISkillDal
{
    public async Task<List<SkillModel>> GetSkills(List<int> skillIds)
    {
        List<SkillModel> skills = await context.Skills.Where(skill => skillIds.Contains(skill.Id)).ToListAsync();
        if (skills.Count != skillIds.Count) throw new Exception("Not all skills found");
        return skills;
    }

    public async Task<List<SkillModel>> GetSkillsFromCategory(int limit, int page, int categoryId)
    {
        if (limit < 0 || page < 0)
        {
            limit = 10;
            page = 0;
        }

        if (categoryId <= 0)
        {
            throw new Exception("Invalid category id");
        }
        return await context.Skills.Where(skill => skill.CategoryId == categoryId).Skip(limit * page).Take(limit).ToListAsync();
    }

    public async Task CreateSkill(SkillModel skill)
    {
        var skillExists = await context.Skills.AnyAsync(s => s.Name == skill.Name);

        if (skillExists)
        {
            throw new Exception($"A skill with the name '{skill.Name}' already exists.");
        }
        
        await context.Skills.AddAsync(skill);
        
        await context.SaveChangesAsync();
    }
}