using DataAccess.Database;
using Interface.Factories;
using Interface.Interface.Dal;

namespace DataAccess.Factories;

public class DalFactory(EasyFlexContext context) : IDalFactory
{
    public ICategoryDal GetCategoryDal()
    {
        return new CategoryDal(context);
    }

    public IFlexWorkerDal GetFlexWorkerDal()
    {
        return new FlexWorkerDal(context);
    }

    public IJobDal GetJobDal()
    {
        return new JobDal(context);
    }

    public ISkillDal GetSkillDal()
    {
        return new SkillDal(context);
    }
}