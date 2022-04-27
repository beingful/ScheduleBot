using Microsoft.Extensions.Caching.Memory;

namespace PrimatScheduleBot
{
    public class DialogueMemory
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public ICommand Get(string key)
        {
            ICommand cacheEntry = default(ICommand);

            _cache.TryGetValue(key, out cacheEntry);

            return cacheEntry;
        }

        public void Set(string key, ICommand item)
        {
            if (item != null)
            {
                _cache.Set(key, item);
            }
        }
    }
}
