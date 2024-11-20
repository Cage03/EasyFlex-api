using DataAccess.Models;
using Interface.Interface.Dal;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class FlexWorkerDal(EasyFlexContext context) : IFlexWorkerDal
{
    public async Task AddFlexWorker(FlexworkerModel flexWorker)
    {
        context.Flexworkers.Add(flexWorker);
        await context.SaveChangesAsync();
    }

    public async Task UpdateFlexWorker(FlexworkerModel flexWorker)
    {
        var existingFlexworker = context.Flexworkers.FirstOrDefaultAsync(worker => worker.Id == flexWorker.Id);
        if (existingFlexworker.Result != null)
        {
            existingFlexworker.Result.Name = flexWorker.Name;
            existingFlexworker.Result.Email = flexWorker.Email;
            existingFlexworker.Result.Address = flexWorker.Address;
            existingFlexworker.Result.DateOfBirth = flexWorker.DateOfBirth;
            existingFlexworker.Result.PhoneNumber = flexWorker.PhoneNumber;
            existingFlexworker.Result.ProfilePictureUrl = flexWorker.ProfilePictureUrl;
        }
        await context.SaveChangesAsync();
    }

    public async Task DeleteFlexWorker(int id)
    {
        var flexWorker = await context.Flexworkers.FindAsync(id);
        context.Flexworkers.Remove(flexWorker!);
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

    public async Task<List<FlexworkerModel>> GetAllFlexWorkers(int limit, int page)
    {
        return await context.Flexworkers.Skip(page * limit).Take(limit).ToListAsync();
    }

    public async Task<FlexworkerModel?> GetFlexWorkerById(int id)
    {
        var flexworker = await context.Flexworkers.Include(f => f.Skills).FirstOrDefaultAsync(f => f.Id == id);
        if (flexworker == null) throw new Exception("Flexworker not found");

        return flexworker;
    }
}