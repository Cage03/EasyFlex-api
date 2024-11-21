using Interface.Models;

namespace Interface.Interface.Dal;

public interface IFlexworkerDal
{
    public Task<List<FlexworkerModel>> GetAllFlexworkers(int limit, int page);
    public Task AddFlexWorker(FlexworkerModel flexworker);
    public Task<FlexworkerModel> GetFlexworkerById(int id);
    public Task UpdateFlexWorker(FlexworkerModel flexworker);
    public Task DeleteFlexworker(int id);
    public Task AddSkills(FlexworkerModel flexworker, List<SkillModel> skills);
    public Task RemoveSkills(FlexworkerModel flexworker, List<SkillModel> skills);
}