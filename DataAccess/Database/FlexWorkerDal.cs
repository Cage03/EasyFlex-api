using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class FlexWorkerDal(dbo context) : IFlexWorkerDal
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
            existingFlexworker.Result.Adress = flexWorker.Adress;
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

    public async Task<List<FlexworkerModel>> GetAllFlexWorkers(int limit, int page)
    {
        return await context.Flexworkers.Skip(page * limit).Take(limit).ToListAsync();
    }

    public async Task<FlexworkerModel?> GetFlexWorkerById(int id)
    {
        var flexworker = await context.Flexworkers.FindAsync(id);
        if (flexworker == null) throw new Exception("Flexworker not found");

        return flexworker;
    }
}