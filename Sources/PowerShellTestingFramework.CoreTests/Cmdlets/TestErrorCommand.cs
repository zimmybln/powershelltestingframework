using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Test.Cmdlets
{
    [Cmdlet(VerbsDiagnostic.Test, "Error")]
    public class TestErrorCommand : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteError(new ErrorRecord(new Exception("Das ist ein Fehler"), "1", ErrorCategory.InvalidOperation, null));
        }
    }
}
