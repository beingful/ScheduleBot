using Microsoft.Extensions.Caching.Memory;

namespace PrimatScheduleBot
{
    public class DialogueMemory
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public ICommand Get(string key)
        {
            ICommand cacheEntry;

            try
            {
                cacheEntry = _cache.Get<ICommand>(key);
            }
            catch
            {
                cacheEntry = null;
            }

            return cacheEntry;
        }

        public void Set(string key, ref ICommand item)
        {
            if (item != null)
            {
                _cache.Set(key, item);
            }
        }
    }
}
