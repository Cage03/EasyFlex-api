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
}