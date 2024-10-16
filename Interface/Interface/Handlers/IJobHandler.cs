using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IJobHandler
{
    public Task<int> CreateJob(JobModel job);
    public Task<JobModel?> GetJob(int id);
    public Task<JobModel[]?> GetJobs(int pageNumber, int limit);
    public Task DeleteJob(int id);
}