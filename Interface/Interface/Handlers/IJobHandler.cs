using Interface.Dtos;
using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IJobHandler
{
    public Task<int> CreateJob(Job job);
    public Task<Job> GetJob(int id);
    public Task<Job[]> GetJobs(int pageNumber, int limit);
    public Task DeleteJob(int id);
    public Task UpdateJob(Job job);
}