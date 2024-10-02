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

    public List<FlexworkerModel> GetAllFlexWorkers()
    {
        return context.Flexworkers.ToList();
    }
    
}