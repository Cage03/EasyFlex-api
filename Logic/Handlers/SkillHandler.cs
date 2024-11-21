using Interface.Dtos;
using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;

namespace Logic.Handlers;

public class SkillHandler(ISkillDal skillDal) : ISkillHandler
{
    public async Task<List<Skill>> GetSkills(List<int> skillIds)
    {
        var skills = await skillDal.GetSkills(skillIds);
        if (skills == null)
        {
            throw new Exception("Not Found");
        }
        return skills.Select(ToDto).ToList();
    }
    
    public static Skill ToDto(SkillModel skillModel)
    {
        return new Skill
        {
            Id = skillModel.Id,
            CategoryId = skillModel.CategoryId,
            Name = skillModel.Name
        };
    }
    public static SkillModel ToModel(Skill skill)
    {
        return new SkillModel
        {
            Id = skill.Id,
            CategoryId = skill.CategoryId,
            Name = skill.Name,
        };
    }
}