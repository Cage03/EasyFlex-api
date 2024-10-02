using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;

namespace DataAccess.Database;

public class JobDal(dbo context) : IJobDal
{
    public async Task CreateJob(JobModel job)
    {
        context.Jobs.Add(job);
        await context.SaveChangesAsync();
    }

    public Task DeleteJob(JobModel job)
    {
        throw new NotImplementedException();
    }

    public Task GetAllJobs()
    {
        throw new NotImplementedException();
    }

    public async Task GetJob(int id)
    {
        await context.Jobs.FindAsync(id);
        await context.SaveChangesAsync();
    }

    public async Task UpdateJob(JobModel job)
    {
        context.Jobs.Update(job);
        await context.SaveChangesAsync();
    }
}