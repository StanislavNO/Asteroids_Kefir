namespace Assets.Source.Code_base
{
    public interface IFactory<T>
    {
        T Create();
    }
}