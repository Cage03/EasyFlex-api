using DataAccess.Models;
using Interface.Interface.Dal;
using Interface.Models;

namespace DataAccess.Database;

public class FlexWorkerDal(dbo context) : IFlexWorkerDal
{
    public async Task AddFlexWorker(FlexworkerModel flexWorker)
    {
        context.Flexworkers.Add(flexWorker);
        await context.SaveChangesAsync();
    }
    public List<FlexworkerModel> GetAllFlexWorkers()
    {
        return context.Flexworkers.ToList();
    }
    
}