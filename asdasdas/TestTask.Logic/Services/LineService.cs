using System;
using System.Collections.Generic;
using System.Linq;

using TestTask.Abstraction.Services;
using TestTask.DataObjects;

namespace TestTask.Logic.Services
{
    public class LineService : ILineService
    {
        public List<SeparatedLine> CheckForIncorrectLines(List<SeparatedLine> separatedLines)
        {
            foreach (var separatedLine in separatedLines)
            {
                decimal number = default;
                if (separatedLine.Elements.All(e => decimal.TryParse(e, out number)))
                {
                    separatedLine.IsCorrect = true;
                }
            }

            return separatedLines;
        }

        public void Test()
        {
            throw new Exception();
        }

        public List<SeparatedLine> GetSplittedLines(List<string> lines)
        {
            var result = new List<SeparatedLine>();

            foreach (var line in lines)
            {
                var separatedLine = new SeparatedLine
                {
                    Elements = line.Split(
                        new char[] { ',' }, 
                        StringSplitOptions.RemoveEmptyEntries)
                    .ToList()
                };

                result.Add(separatedLine);
            }

            return result;
        }

        public int GetLineNumberWithMaxElementSum(List<SeparatedLine> separatedLines)
        {
            var lineWithMaxSum = separatedLines
                .Where(sl => sl.IsCorrect)
                .OrderByDescending(
                    sl => sl.Elements.Sum(e => decimal.Parse(e)))
                .FirstOrDefault();

            return separatedLines.IndexOf(lineWithMaxSum ?? new SeparatedLine());
        }
    }
}