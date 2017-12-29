namespace BusinessLogic.Service.Base
{
    public interface IGameService<T>
    {
        string Serialize(T obj);
        T Deserialize(string obj);
    }
}