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

    public IFlexworkerDal GetFlexWorkerDal()
    {
        return new FlexworkerDal(context);
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