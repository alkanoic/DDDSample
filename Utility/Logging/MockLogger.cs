using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Logging
{
    public class MockLogger : ILogger
    {
        public void Write(LogLevel level, string log, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
        }

        public void Write(LogLevel level, Exception log, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
        }

        public void Write(LogLevel level, string message, object param, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
        }

        public void WriteSql(LogLevel level, string sql, object param, [CallerMemberName] string memberName = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = -1)
        {
        }
    }
}
