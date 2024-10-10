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
        var existingFlexworker = context.Flexworkers.FindAsync(flexWorker.Id);
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
        var flexWorker = context.Flexworkers.Find(id);
        context.Flexworkers.Remove(flexWorker);
        await context.SaveChangesAsync();
    }

    public async Task<List<FlexworkerModel>> GetAllFlexWorkers(int limit, int page)
    {
        return context.Flexworkers.Skip(page * limit).Take(limit).ToList();
    }

    public FlexworkerModel GetFlexWorkerById(int id)
    {
        return context.Flexworkers.Find(id);
    }
}