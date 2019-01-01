using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitExample
{
    class CPUWorker
    {
        int? classID = null;

        public CPUWorker(int ID)
        {
            classID = ID;
        }

        public int? ClassID
        {
            get
            {
                return classID;
            }
        }

        public async Task<int> DoSomeWork(int x)
        {
            Random rnd = new Random();
            int month = rnd.Next(1, 13);  // 1 <= month < 13

            Console.WriteLine("Do CPU work in Thread {0}.", System.Threading.Thread.CurrentThread.ManagedThreadId);

            await Task.Delay(2000);

            return month * x;
        }
    }
}
