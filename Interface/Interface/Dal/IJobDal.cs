using Interface.Models;

namespace Interface.Interface.Dal;

public interface IJobDal
{
    public Task<int> CreateJob(JobModel job);
    public Task<JobModel> GetJob(int id);
    public Task<List<JobModel>> GetJobsBySkillIds(List<int> skillIds);
    public Task<List<JobModel>> GetJobs(int offset, int limit);
    public Task UpdateJob(JobModel job);
    public Task DeleteJob(int id);
}