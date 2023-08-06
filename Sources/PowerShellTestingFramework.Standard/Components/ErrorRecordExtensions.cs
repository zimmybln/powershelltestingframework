using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace PowerShellTestingFramework.Components
{
    public static class ErrorRecordExtensions
    {
        public static bool Contains<T>(this ErrorRecord errorRecord)
            where T : Exception
        {
            if (errorRecord == null || errorRecord.Exception == null)
                return false;

            var exception = errorRecord.Exception;

            while (exception != null)
            {
                if (exception.GetType() == typeof(T))
                    return true;

                exception = exception.InnerException;
            }
            
            return false;
        }
    }
}
