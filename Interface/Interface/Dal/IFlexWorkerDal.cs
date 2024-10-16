using Interface.Models;

namespace Interface.Interface.Dal;

public interface IFlexWorkerDal
{
    public Task<List<FlexworkerModel>> GetAllFlexWorkers(int limit, int page);
    public Task AddFlexWorker(FlexworkerModel flexWorker);
    public FlexworkerModel GetFlexWorkerById(int id);
    public Task UpdateFlexWorker(FlexworkerModel flexWorker);
    public Task DeleteFlexWorker(int id);
}