using System.Collections.Generic;

using TestTask.DataObjects;

namespace TestTask.Abstraction.Services
{
    public interface ILineService
    {
        List<SeparatedLine> GetSplittedLines(List<string> lines);

        List<SeparatedLine> CheckForIncorrectLines(List<SeparatedLine> separatedLines);

        int GetLineNumberWithMaxElementSum(List<SeparatedLine> separatedLines);
    }
}