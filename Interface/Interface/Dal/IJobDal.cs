using Interface.Models;

namespace Interface.Interface.Dal;

public interface IJobDal
{
    public Task CreateJob(JobModel job);
    public Task GetJob(int id);
    public Task GetAllJobs();
    public Task UpdateJob(JobModel job);
    public Task DeleteJob(JobModel job);
}