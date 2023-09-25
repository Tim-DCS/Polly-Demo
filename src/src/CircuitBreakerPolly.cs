using Polly;

namespace Polly_Demo.src
{
    public class CircuitBreakerPolly
    {
        public void RunCircuitBreaker()
        {
            // 创建一个断路器策略  
            var circuitBreakerPolicy = Policy
                .Handle<TimeoutException>() // 捕获HttpRequestException异常  
                .CircuitBreaker(5, TimeSpan.FromSeconds(30)); // 设置断路器参数：失败次数为5，冷却时间为30秒

            for (int i = 0; i < 10; i++) 
            {
                try
                {
                    Console.WriteLine($"执行第{i+1} 次， 时间：{DateTime.Now}");
                    circuitBreakerPolicy.Execute(ExecuteMockRequest);
                }
                catch (Polly.CircuitBreaker.BrokenCircuitException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (TimeoutException) 
                {
                    Console.WriteLine("TimeOut");
                }
            }
        }

        private string ExecuteMockRequest()
        {
            // 模拟网络请求
            Console.WriteLine("正在模拟网络请求...");
            Thread.Sleep(2000);
            // 模拟网络错误
            throw new TimeoutException();
        }
    }
}
