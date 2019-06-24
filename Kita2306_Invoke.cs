using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kita2306_Invoke
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Func<int> func = new Func<int>(() => {
                int number = random.Next(1000);
                Thread.Sleep(number);
                Console.WriteLine(DateTime.Now);
                return DateTime.Now.Millisecond;
            });

            object o1 = func.DynamicInvoke();
            Console.WriteLine(o1);

            Console.WriteLine("========Q2:");
            IAsyncResult async = func.BeginInvoke((IAsyncResult ar) =>
            {
                Console.WriteLine("after..........");

                Console.WriteLine(ar.AsyncState); // this is the "hello" parameter

                int res = func.EndInvoke(ar); // pull result in callback method
                Console.WriteLine("what was the result? " + res);

            }, "hello");
            Thread.Sleep(3000);
            //async.IsCompleted
            int result = func.EndInvoke(async);
            Console.WriteLine("what was the result? " + result);

            foreach (Func<int> method in func.GetInvocationList())
            {
                method.BeginInvoke(null, null);
            }
        }
    }
}
