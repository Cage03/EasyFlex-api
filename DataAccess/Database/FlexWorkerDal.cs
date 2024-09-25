using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Database;

public class FlexWorkerDal(dbo context)
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