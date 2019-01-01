using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AsyncAwaitExample
{
    class Program
    {
        static int classNumber = 0;

        static void Main(string[] args)
        {
            var tasks = new List<Task<int>>();
            classNumber = 1;

            IOWorker iow = new IOWorker(classNumber);
            ++classNumber;

            CPUWorker cpuw = new CPUWorker(classNumber);
            ++classNumber;

            // start io tasks on the same thread as the UI
            Task<int> ioTaskOutput = iow.DoSomeWork(3);
            tasks.Add(ioTaskOutput);

            // start cpu tasks on their own thread.
            Task<int> cpuTaskOutput = Task.Run(() => cpuw.DoSomeWork(3));
            tasks.Add(cpuTaskOutput);

            DoIndependentWork();

            Console.WriteLine("IO Task Result: {0}", ioTaskOutput.Result);

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine(string.Format("The sum of the results is {0}", ioTaskOutput.Result + cpuTaskOutput.Result));

            Console.WriteLine("CPU Task Result: {0}", cpuTaskOutput.Result);

            // Wait for all the tasks to finish.

            Console.ReadLine();
        }

        static void DoIndependentWork()
        {
            Console.WriteLine("DoIndependentWork in Thread {0}.", System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

    }
}
