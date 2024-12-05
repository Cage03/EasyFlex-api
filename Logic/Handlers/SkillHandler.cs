using Interface.Dtos;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class SkillHandler(ISkillDal skillDal) : ISkillHandler
{
    public async Task<Skill> GetSkillFromId(int id)
    {
        var skill = await skillDal.GetSkill(id);
        return ToDto(skill);
    }


    public async Task<List<Skill>> GetSkillsFromCategory(int limit, int page, int categoryId)
    {
        if (categoryId <= 0)
        {
            throw new Exception("Invalid category id");
        }
        var skillModels = await skillDal.GetSkillsFromCategory(limit, page, categoryId);
        return skillModels.Select(ToDto).ToList();
    }


    public async Task CreateSkill(string name, int categoryId)
    {
        await skillDal.CreateSkill(name, categoryId);
    }

    public async Task DeleteSkill(int id)
    {
        await skillDal.DeleteSkill(id);
    }

    public async Task UpdateSkill(Skill skill)
    {
        if (skill.Id <= 0) throw new Exception("Invalid or no skill id provided");
        
        await skillDal.UpdateSkill(ToModel(skill));
    }

    public static Skill ToDto(SkillModel model)
    {
        return new Skill
        {
            Id = model.Id,
            Name = model.Name,
            CategoryId = model.CategoryId
        };
    }
    
    public static SkillModel ToModel(Skill skill)
    {
        return new SkillModel
        {
            Id = skill.Id,
            Name = skill.Name,
            CategoryId = skill.CategoryId
        };
    }
}