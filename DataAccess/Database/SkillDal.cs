using Interface.Exceptions;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class SkillDal(EasyFlexContext context) : ISkillDal
{
    public async Task<SkillModel> GetSkill(int id)
    {
        var skill = await context.Skills.FindAsync(id);

        if (skill == null)
        {
            throw new NotFoundException($"Skill with id '{id}' not found.");
        }

        return skill;
    }

    public async Task<List<SkillModel>> GetSkillsFromCategory(int limit, int page, int categoryId)
    {
        if (limit < 0 || page < 0)
        {
            limit = 10;
            page = 0;
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
            throw new NotFoundException($"Skill with id '{id}' not found.");
        }

        context.Skills.Remove(skill);
        await context.SaveChangesAsync();
    }

    public async Task UpdateSkill(SkillModel skill)
    {
        context.Skills.Update(skill);
        await context.SaveChangesAsync();
    }

}