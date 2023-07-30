using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellSampleComponents.Cmdlets
{
    [Cmdlet(VerbsCommunications.Write, "Something")]
    public class WriteSomething : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            WriteVerbose("Das ist ein Test");
            WriteWarning("Das ist eine Warnung");
        }
    }
}
