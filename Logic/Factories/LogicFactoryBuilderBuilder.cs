using Interface.Factories;

namespace Logic.Factories;

public class LogicFactoryBuilderBuilder(IDalFactory dalFactory) : ILogicFactoryBuilder
{
    private readonly IDalFactory _dalFactory = dalFactory;


    public IHandlerFactory BuildHandlerFactory()
    {
        return new HandlerFactory(dalFactory);
    }
}