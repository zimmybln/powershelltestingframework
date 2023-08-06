using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Tests.Cmdlets
{
    [Cmdlet(VerbsDiagnostic.Test, "Parameters")]
    public class TestParameters : PSCmdlet
    {
        [Parameter(Position=0)]
        [ValidateLength(5, Int32.MaxValue)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            // no processing here 
            // all validation should be done here
            
            base.ProcessRecord();
        }
    }
}
