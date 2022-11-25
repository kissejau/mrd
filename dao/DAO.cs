public interface DAO<T>
{
    List<T> List();
    void Create(T t);

    T Get(string id);

    bool Update(T t, string id);

    bool Delete(string id);
}

