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
        var existingFlexworker = await context.Flexworkers.FirstOrDefaultAsync(worker => worker.Id == flexWorker.Id);
        if (existingFlexworker != null)
        {
            existingFlexworker.Name = flexWorker.Name;
            existingFlexworker.Email = flexWorker.Email;
            existingFlexworker.DateOfBirth = flexWorker.DateOfBirth;
            existingFlexworker.PhoneNumber = flexWorker.PhoneNumber;
            existingFlexworker.ProfilePictureUrl = flexWorker.ProfilePictureUrl;
            
            //context.Flexworkers.Update(existingFlexworker);
        }
        await context.SaveChangesAsync();
    }

    public List<FlexworkerModel> GetAllFlexWorkers()
    {
        return context.Flexworkers.ToList();
    }

    public FlexworkerModel GetFlexWorkerById(int id)
    {
        return context.Flexworkers.Find(id);
    }
}