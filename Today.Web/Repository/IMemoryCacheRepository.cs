namespace Today.Web.Repository
{
    public interface IMemoryCacheRepository
    {
        void Set<T>(string key, T value) where T : class;
        T Get<T>(string key) where T : class;
        void Remove(string key);
    }
}
