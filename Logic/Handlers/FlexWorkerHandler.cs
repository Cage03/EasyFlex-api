using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Classes;


namespace Logic.Handlers;

public class FlexWorkerHandler(IFlexWorkerDal flexWorkerDal) : IFlexWorkerHandler
{
    public async Task CreateFlexWorker(FlexworkerModel flexWorker)
    { 
        await flexWorkerDal.AddFlexWorker(flexWorker);
    }

    public async Task<List<FlexworkerModel>> GetFlexWorkers(int limit, int page)
    {
        return await flexWorkerDal.GetFlexWorkersByPage(limit, page);
    }

    public async Task<FlexworkerModel?> GetFlexworkerById(int id)
    {
        return await flexWorkerDal.GetFlexWorkerById(id);
    }

    public async Task DeleteFlexWorker(int id)
    {
        var flexWorker = await GetFlexworkerById(id);
        if (flexWorker == null)
        {
            throw new Exception("Flexworker not found");
        }
        await flexWorkerDal.DeleteFlexWorker(id);
    }

    public async Task UpdateFlexWorker(FlexworkerModel flexWorker)
    {
        await flexWorkerDal.UpdateFlexWorker(flexWorker);
    }
}