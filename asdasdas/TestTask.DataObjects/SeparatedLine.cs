using System.Collections.Generic;

namespace TestTask.DataObjects
{
    public class SeparatedLine
    {
        public List<string> Elements { get; set; }

        public bool IsCorrect { get; set; } = false;
    }
}