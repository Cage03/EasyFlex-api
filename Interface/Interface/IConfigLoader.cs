namespace Interface.Interface;

public interface IConfigLoader
{
    public T GetConfig<T>();
}