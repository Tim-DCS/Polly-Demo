using Polly;

namespace Polly_Demo.src
{
    public class TimeOutPolly
    {
        public void RunTimeoutPolicy()
        {
            // 定义回退策略
            ISyncPolicy fallback = Policy.Handle<Polly.Timeout.TimeoutRejectedException>()
                     .Or<ArgumentException>()
                     .Fallback(() =>
                     {
                         Console.WriteLine("Error occured,runing fallback");
                     },
                     (ex) => { Console.WriteLine($"Fallback exception:{ex.GetType().ToString()},Message:{ex.Message}"); });

            // 定义超时策略
            ISyncPolicy policyTimeout = Policy.Timeout(3, Polly.Timeout.TimeoutStrategy.Pessimistic);

            while (true)
            {
                // Wrap: 将回退策略和超时策略组合并生成Policy实例。
                var policyWrap = Policy.Wrap(fallback, policyTimeout);
                policyWrap.Execute(() =>
                {
                    Console.WriteLine("Job Start");
                    if (DateTime.Now.Second % 2 == 0)
                    {
                        Thread.Sleep(5000);
                    }
                });
                Thread.Sleep(300);
            }
        }
    }
}
