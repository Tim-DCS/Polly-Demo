using Polly;
using System.Net;

namespace Polly_Demo.src
{
    public class ExceptionPolly
    {
        public void Execute() 
        {
            Policy.Handle<HttpRequestException>().OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.BadGateway || r.StatusCode == HttpStatusCode.BadRequest)
                .Retry(3, (exception, retryCount, context) =>
                {
                    Console.WriteLine($"开始第 {retryCount} 次重试：");
                })
                .Execute(ExecuteMockRequest);

            Console.WriteLine("程序结束，按任意键退出。");
            Console.ReadKey();
        }

        public void CommExecute()
        {
            Policy.HandleResult<Exception>(r => r.Message == "111")
                .Retry(3, (exception, retryCount, context) =>
                {
                    Console.WriteLine($"开始第 {retryCount} 次重试：");
                })
                .Execute(CommExecuteRequest);

            Console.WriteLine("程序结束，按任意键退出。");
            Console.ReadKey();
        }

        private HttpResponseMessage ExecuteMockRequest()
        {
            // 模拟网络请求
            Console.WriteLine("正在执行网络请求...");
            Thread.Sleep(3000);
            // 模拟网络错误
            return new HttpResponseMessage(HttpStatusCode.BadGateway);
        }

        private Exception CommExecuteRequest()
        {
            Console.WriteLine("正在执行业务逻辑...");
            Thread.Sleep(3000);
            Exception exception = new Exception("111");
            return exception;
        }
    }
}
