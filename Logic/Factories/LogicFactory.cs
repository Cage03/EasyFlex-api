using Interface.Factories;

namespace Logic.Factories;

public class LogicFactory : ILogicFactory
{
    private readonly IDalFactory _dalFactory;

    public LogicFactory(IDalFactory dalFactory)
    {
        _dalFactory = dalFactory;
    }
}