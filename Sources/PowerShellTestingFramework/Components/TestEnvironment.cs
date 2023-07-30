using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Components
{
    public static class TestEnvironment
    {
        public static string GetFileContent(this object item, string filePath, Encoding encoding = null)
        {
            if (item == null)
                return null;

            string fileName = Path.Combine(Path.GetDirectoryName(new Uri(item.GetType().Assembly.CodeBase).LocalPath), filePath);

            if (!File.Exists(fileName))
                throw new FileNotFoundException("File not exists", fileName);

            if (encoding == null)
                encoding = Encoding.UTF8;

            return File.ReadAllText(fileName, encoding);
        }
    }
}
