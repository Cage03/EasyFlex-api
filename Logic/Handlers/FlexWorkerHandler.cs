using Interface.Interface.Dal;
using Interface.Interface.Handlers;
using Interface.Models;
using Logic.Classes;


namespace Logic.Handlers;

public class FlexWorkerHandler(IFlexWorkerDal flexWorkerDal) : IFlexWorkerHandler
{
    public async Task CreateFlexWorker(FlexWorker flexWorker)
    { 
        await flexWorkerDal.AddFlexWorker(flexWorker.ToModel());
    }

    public List<FlexworkerModel> GetFlexWorkers()
    {
        return flexWorkerDal.GetAllFlexWorkers();
    }
}