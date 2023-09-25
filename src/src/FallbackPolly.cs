using Polly;

namespace Polly_Demo.src
{
    public class FallbackPolly
    {
        public void RunFallbackPolly()
        {
            var PolicyExecute = Policy.Handle<Exception>().Fallback(() =>
            {
                Console.WriteLine($"你的程序报错了，我是替代程序！");

            });

            //执行
            PolicyExecute.Execute(() =>
            {
                Console.WriteLine("开始执行程序");
                Console.WriteLine("");
                throw new Exception();
            });

            Console.Read();
        }
    }
}
