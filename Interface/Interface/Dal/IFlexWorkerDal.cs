using Interface.Models;

namespace Interface.Interface.Dal;

public interface IFlexworkerDal
{
    public Task<List<FlexworkerModel>> GetAllFlexworkers(int limit, int page);
    public Task AddFlexWorker(FlexworkerModel flexworker);
    public Task<FlexworkerModel> GetFlexworkerById(int id);
    public Task<List<FlexworkerModel>> GetFlexworkersBySkills(List<int> skillIds);
    public Task UpdateFlexworker(FlexworkerModel flexworker);
    public Task DeleteFlexworker(int id);
    public Task AddSkills(int flexworkerId, List<SkillModel> skills);
    public Task RemoveSkills(int flexworkerId, List<SkillModel> skills);
}