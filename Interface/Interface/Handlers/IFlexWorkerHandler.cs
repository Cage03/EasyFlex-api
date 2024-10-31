using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IFlexWorkerHandler
{
    public Task<List<FlexworkerModel>> GetFlexWorkers(int limit, int page);
    public Task CreateFlexWorker(FlexworkerModel flexWorker);
    public Task UpdateFlexWorker(FlexworkerModel flexWorker);
    public Task<FlexworkerModel> GetFlexworkerById(int id);
    public Task DeleteFlexWorker(int id);
    public Task AddSkills(int flexWorkerId, List<SkillModel> skills);
    public Task DeleteSkills(int flexWorkerId, List<SkillModel> skills);
}