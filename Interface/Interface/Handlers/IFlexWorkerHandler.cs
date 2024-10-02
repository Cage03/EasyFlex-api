using Interface.Models;

namespace Interface.Interface.Handlers;

public interface IFlexWorkerHandler
{
    public List<FlexworkerModel> GetFlexWorkers();
    public Task CreateFlexWorker(FlexworkerModel flexWorker);
    public FlexworkerModel SelectFlexworkerById(int id);
    
    public Task UpdateFlexWorker(FlexworkerModel flexWorker);
}