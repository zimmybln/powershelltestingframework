using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Test.Cmdlets
{
    [Cmdlet(VerbsCommon.Show, "TypesOfInformation")]
    public class ShowTypesOfInformationCommand : PSCmdlet
    {

        protected override void ProcessRecord()
        {
            WriteError(new ErrorRecord(new Exception("Eine Ausnahme"), "1", ErrorCategory.NotImplemented, null));

            WriteVerbose("Verbose");
            WriteDebug("Debug");
            WriteInformation("Information", null);
            WriteWarning("Warning");
            

            WriteCommandDetail("Das sind Details über das Command");

        }
    }
}
