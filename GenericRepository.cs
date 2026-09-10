public class GenericRepository<T> where T : class, IIdentifiable
{
    private readonly Dictionary<int, T> _items = new();

    public void Add(T item)
    {
        _items.Add(item.Id, item);
    }

    public T? GetById(int id)
    {
        _items.TryGetValue(id, out T? item);
        return item;
    }

    public bool Update(T item)
    {
        if (!_items.ContainsKey(item.Id))
        {
            return false;
        }

        _items[item.Id] = item;
        return true;
    }

    public bool Delete(int id)
    {
        return _items.Remove(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _items.Values;
    }
}
