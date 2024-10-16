using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IFlexWorkerHandler
{
    public Task<List<FlexworkerModel>> GetFlexWorkers(int limit, int page);
    public Task CreateFlexWorker(FlexworkerModel flexWorker);
    public Task UpdateFlexWorker(FlexworkerModel flexWorker);
    public FlexworkerModel GetFlexworkerById(int id);
    public Task DeleteFlexWorker(int id);
}