using Interface.Dtos;
using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IFlexworkerHandler
{
    public Task<List<Flexworker>> GetFlexworkers(int limit, int page);
    public Task<List<Flexworker>> GetFlexworkersBySkillIds(List<int> skillIds);
    public Task CreateFlexworker(Flexworker flexworker);
    public Task UpdateFlexworker(Flexworker flexworker);
    public Task<Flexworker> GetFlexworkerById(int id);
    public Task DeleteFlexworker(int id);
    public Task AddSkills(int flexworkerId, List<Skill> skills);
    public Task RemoveSkills(int flexworkerId, List<Skill> skills);
}