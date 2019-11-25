using System;
using System.Linq;

using TestTask.Logic.Managers;
using TestTask.Logic.Services;

namespace TestTask.ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Write the file path");

                var filePath = Console.ReadLine();

                var fileManager = FileManager.Instatnce;

                var lines = fileManager.ReadLines(filePath);

                var lineSevice = new LineService();
                Console.WriteLine("blabla");
                Console.WriteLine("not visible");
                var separatedLines = lineSevice.GetSplittedLines(lines);
                var checkedLines = lineSevice.CheckForIncorrectLines(separatedLines);
                var lineNumberWithMaxElementSum = lineSevice.GetLineNumberWithMaxElementSum(checkedLines);

                if (lineNumberWithMaxElementSum == -1)
                {
                    Console.WriteLine("There is no lines that contains just numbers");
                }
                else
                {
                    Console.WriteLine($"Line number with max element sum is {lineNumberWithMaxElementSum + 1}");
                }

                if (checkedLines.Any(cl => !cl.IsCorrect))
                {
                    Console.WriteLine("Lines with incorrect data :");

                    foreach (var incorrectLine in checkedLines.Where(cl => !cl.IsCorrect))
                    {
                        Console.WriteLine(string.Join(",", incorrectLine.Elements));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press any key to close the program");
            Console.ReadKey();
        }
    }
}