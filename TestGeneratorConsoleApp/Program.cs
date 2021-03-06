using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestGeneratorConsoleApp
{
    class Program
    { 
        static List<string> InputFilePaths()
        {
            List<string> result = new List<string>();

            string inputValue = Console.ReadLine();
            while (inputValue != "")
            {
                result.Add(inputValue);
                inputValue = Console.ReadLine();
            }

            return result;
        }

        static async Task Main(string[] args)
        {
            
            Console.WriteLine("Введите пути к файлам для генерации тестов:");
            List<string> files = InputFilePaths();
            Console.WriteLine("Введите папку для вывода:");
            string folder = Console.ReadLine();
            Console.WriteLine("Введите параметры конвейера:");
            int[] pipelineParams = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Int32.TryParse(Console.ReadLine(), out pipelineParams[i]);
            }

            Pipeline pipeline = new Pipeline(folder);
            pipeline.SetParameters(pipelineParams[0], pipelineParams[1], pipelineParams[3]);
            

            /*Pipeline pipeline = new Pipeline("C:\\Users\\anast\\source\\repos\\TestGenerator\\Results");
            pipeline.SetParameters(2, 2, 3);
            List<string> files = new List<string>() { "C:\\Users\\anast\\source\\repos\\TestGenerator\\Examples\\TreeClass.cs" };*/
            await pipeline.Processing(files);
        }
    }
}
