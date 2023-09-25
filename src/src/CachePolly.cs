using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;

namespace Polly_Demo.src
{
    public class CachePolly
    {
        public void RunCachePolly()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            ISyncCacheProvider memoryCacheProvider = new MemoryCacheProvider(memoryCache);
            var cachePolicy = Policy.Cache<string>(memoryCacheProvider, TimeSpan.FromSeconds(5));

            var result = cachePolicy.Execute(context => DoSomething(), new Context("foo"));
            Console.WriteLine($"DoSomething 函数返回值： {result}");

            Thread.Sleep(1000);
            result = cachePolicy.Execute(context => DoSomething(), new Context("foo"));
            Console.WriteLine($"DoSomething 函数返回值： {result}");

            Thread.Sleep(5000);
            result = cachePolicy.Execute(context => DoSomething(), new Context("foo"));
            Console.WriteLine($"DoSomething 函数返回值： {result}");
            Console.Read();
        }

        private string DoSomething()
        {
            Console.WriteLine("调用DoSomething.................");
            return "Hello world";
        }
    }
}
