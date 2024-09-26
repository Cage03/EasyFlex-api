using DataAccess.Models;
using Interface.Interface.Dal;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class FlexWorkerDal(dbo context) : IFlexWorkerDal
{
    public async Task AddFlexWorker(Flexworker flexworker)
    {
        context.Flexworkers.Add(flexworker);
        await context.SaveChangesAsync();
    }
    public List<Flexworker> GetAllFlexWorkers()
    {
        return context.Flexworkers.ToList();
    }
}