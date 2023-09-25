using Polly;

namespace Polly_Demo.src
{
    public class BulkheadPolly
    {
        /// <summary>
        /// maxParallelization 是可以并发执行的操作
        /// maxQueuingActions 是可以排队的执行的个数
        /// 如果请求量超出了 maxParalleization，那么后续的请求就会被 reject 掉。
        /// </summary>
        /// <returns></returns>
        public async Task RunBulkheadPolly()
        {
            var policy = Policy.BulkheadAsync(5, 2, onBulkheadRejectedAsync: (context) =>
            {
                Console.WriteLine("执行隔离...");
                return Task.CompletedTask;
            });

            for (int i = 0; i < 100; i++)
            {
                _ = policy.ExecuteAsync(async ct =>
                {
                    Console.WriteLine($"{DateTime.Now}");
                    await DoSomething();
                }, CancellationToken.None);
            }

            await Task.CompletedTask;
            Console.Read();
        }

        private async Task DoSomething()
        {
            Console.WriteLine("调用DoSomething.................");
            await Task.Delay(1000);
        }
    }
}
