using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Resources;
using System.Reflection;

namespace AsyncAwaitExample
{
    public class IOWorker
    {
        int? classID = null;

        public IOWorker(int ID)
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
            // read file from disk
            try
            {
                string dataString = string.Empty;

                Console.WriteLine("Do IO work in Thread {0}.", System.Threading.Thread.CurrentThread.ManagedThreadId);

                if (File.Exists("..\\..\\SomeDataFile.xml") == true)
                {
                    //Open the stream and read it back.
                    using (StreamReader reader = File.OpenText("..\\..\\SomeDataFile.xml"))
                    {
                        Console.WriteLine("Opened file.");
                        dataString = await reader.ReadToEndAsync();
                        Console.WriteLine("read contents with length " + dataString.Length.ToString());
                    }
                }

                return dataString.Length;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not read the file",ex);
            }           

        }

    }
}
