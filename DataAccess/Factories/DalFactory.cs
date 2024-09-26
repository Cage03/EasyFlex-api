using DataAccess.Database;
using DataAccess.Models;
using Interface.Factories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Factories;

public class DalFactory(dbo context) : IDalFactory
{
    public FlexWorkerDal CreateFlexWorkerDal()
    {
        return new FlexWorkerDal(context);
    }
    public SkillDal CreateSkillDal()
    {
        return new SkillDal(context);
    }
    public JobDal CreateJobDal()
    {
        return new JobDal(context);
    }
    public CategoryDal CreateCategoryDal()
    {
        return new CategoryDal(context);
    }
}