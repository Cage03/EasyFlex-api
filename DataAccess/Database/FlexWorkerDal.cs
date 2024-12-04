using Interface.Exceptions;
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
        {
            throw new NotFoundException("Flexworker not found");
        }

        return flexworker;
    }

    public async Task<List<FlexworkerModel>> GetFlexworkersBySkills(List<int> skillIds)
    {
        List<FlexworkerModel> flexworkers = await context.Flexworkers
            .Include(f => f.Skills)
            .Where(f => skillIds.All(id => f.Skills.Any(s => s.Id == id)))
            .ToListAsync();

        if (flexworkers.Count == 0)
        {
            throw new NotFoundException("No flexworkers found with the given skills");
        }
        return flexworkers;
    }

    public async Task UpdateFlexworker(FlexworkerModel flexworker)
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

    public async Task AddSkills(int flexworkerId, List<SkillModel> skills)
    {
        foreach (var skill in skills)
        {
            var flexworker = await GetFlexworkerById(flexworkerId);
            var existingSkill = await context.Skills.FirstOrDefaultAsync(s => s.Id == skill.Id);
            if (existingSkill != null)
            {
                flexworker.Skills.Add(existingSkill);
            }
        }

        await context.SaveChangesAsync();
    }

    public async Task RemoveSkills(int flexworkerId, List<SkillModel> skills)
    {
        var flexworker = await GetFlexworkerById(flexworkerId);
        foreach (var skill in skills)
        {
            var existingSkill = context.Skills.FirstOrDefaultAsync(s => s.Id == skill.Id);
            if (existingSkill.Result != null)
            {
                flexworker.Skills.Remove(existingSkill.Result);
            }
        }

        await context.SaveChangesAsync();
    }
}