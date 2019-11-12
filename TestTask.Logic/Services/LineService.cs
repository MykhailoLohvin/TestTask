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

        public List<SeparatedLine> GetSplittedLines(List<string> lines)
        {
            var result = new List<SeparatedLine>();

            foreach (var line in lines)
            {
                var separatedLine = new SeparatedLine
                {
                    Elements = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                };

                result.Add(separatedLine);
            }

            return result;
        }

        public int GetLineNumberWithMaxElementSum(List<SeparatedLine> separatedLines)
        {
            decimal maxSum = decimal.MinValue;
            int lineNUmber = default;

            foreach (var separatedLine in separatedLines.Where(sl => sl.IsCorrect))
            {
                var lineSum = separatedLine.Elements.Sum(e => decimal.Parse(e));

                if (lineSum >= maxSum)
                {
                    maxSum = lineSum;
                    lineNUmber = separatedLines.IndexOf(separatedLine);
                }
            }

            return lineNUmber;
        }
    }
}