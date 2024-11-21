using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class FlexworkerDal(EasyFlexContext context) : IFlexworkerDal
{
    public async Task AddFlexWorker(FlexworkerModel flexworker)
    {
        context.Flexworkers.Add(flexworker);
        await context.SaveChangesAsync();
    }

    public async Task<List<FlexworkerModel>> GetAllFlexworkers(int limit, int page)
    {
        return await context.Flexworkers.Skip(page * limit).Take(limit).ToListAsync();
    }

    public async Task<FlexworkerModel> GetFlexworkerById(int id)
    {
        var flexworker = await context.Flexworkers.Include(f => f.Skills).FirstOrDefaultAsync(f => f.Id == id);
        if (flexworker == null)
        { throw new Exception("Flexworker not found"); }

        return flexworker;
    }

    public async Task UpdateFlexWorker(FlexworkerModel flexworker)
    {
        var existingFlexworker = context.Flexworkers.FirstOrDefaultAsync(worker => worker.Id == flexworker.Id);
        if (existingFlexworker.Result != null)
        {
            existingFlexworker.Result.Name = flexworker.Name;
            existingFlexworker.Result.Email = flexworker.Email;
            existingFlexworker.Result.Address = flexworker.Address;
            existingFlexworker.Result.DateOfBirth = flexworker.DateOfBirth;
            existingFlexworker.Result.PhoneNumber = flexworker.PhoneNumber;
            existingFlexworker.Result.ProfilePictureUrl = flexworker.ProfilePictureUrl;
        }
        await context.SaveChangesAsync();
    }

    public async Task DeleteFlexworker(int id)
    {
        var flexworker = await context.Flexworkers.FindAsync(id);
        context.Flexworkers.Remove(flexworker!);
        await context.SaveChangesAsync();
    }

    public async Task AddSkills(FlexworkerModel flexworker, List<SkillModel> skills)
    {
        foreach (var skill in skills)
        {
            var existingSkill = context.Skills.FirstOrDefaultAsync(s => s.Id == skill.Id);
            if (existingSkill.Result != null)
            {
                flexworker.Skills.Add(existingSkill.Result);
            }
        }
        await context.SaveChangesAsync();
    }

    public async Task RemoveSkills(FlexworkerModel flexWorker, List<SkillModel> skills)
    {
        foreach (var skill in skills)
        {
            var existingSkill = context.Skills.FirstOrDefaultAsync(s => s.Id == skill.Id);
            if (existingSkill.Result != null)
            {
                flexWorker.Skills.Remove(existingSkill.Result);
            }
        }
        await context.SaveChangesAsync();
    }
}