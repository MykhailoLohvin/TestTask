using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestTask.Logic.Managers
{
    public class FileManager
    {
        #region Singletone
        private static FileManager instance;

        public static FileManager Instatnce => instance ??= new FileManager();

        private FileManager()
        {

        }
        #endregion

        public List<string> ReadLines(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException("Incorrect file path");
            }

            var lines = File.ReadAllLines(filePath);

            return lines.ToList();
        }
    }
}