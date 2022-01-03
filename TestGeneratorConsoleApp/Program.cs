using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestGeneratorConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Pipeline pipeline = new Pipeline("C:\\Users\\anast\\source\\repos\\TestGenerator\\Results");
            pipeline.SetParameters(2, 2, 3);
            List<string> files = new List<string>() { "C:\\Users\\anast\\source\\repos\\TestGenerator\\Examples\\TreeClass.cs" };
            await pipeline.Processing(files);
        }
    }
}
