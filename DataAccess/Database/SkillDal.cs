using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class SkillDal(dbo context):ISkillDal
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

    public async Task CreateSkill(string name, int categoryId)
    {
        var skillExists = await context.Skills.AnyAsync(s => s.Name == name);

        if (skillExists)
        {
            throw new Exception($"A skill with the name '{name}' already exists.");
        }
        
        await context.Skills.AddAsync(new SkillModel()
        {
            Name = name,
            CategoryId = categoryId
        });
        
        await context.SaveChangesAsync();
    }

    public async Task DeleteSkill(int id)
    {
        var skill = await context.Skills.FindAsync(id);

        if (skill == null)
        {
            throw new Exception($"Skill with id '{id}' not found.");
        }

        context.Skills.Remove(skill);
        await context.SaveChangesAsync();
    }
}