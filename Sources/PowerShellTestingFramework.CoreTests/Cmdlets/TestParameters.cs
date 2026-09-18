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

        [Parameter]
        [ValidateRange(0, 120)]
        public int Age { get; set; }

        [Parameter]
        [ValidateSet("Red", "Green", "Blue")]
        public string Color { get; set; }

        [Parameter]
        [ValidatePattern(@"^[A-Z]{3}[0-9]{3}$")]
        public string Code { get; set; }

        [Parameter]
        [ValidateCount(1, 3)]
        public string[] Items { get; set; }

        [Parameter]
        [ValidateNotNullOrEmpty]
        public string RequiredValue { get; set; }

        [Parameter]
        [ValidateNotNull]
        public object Data { get; set; }

        protected override void ProcessRecord()
        {
            // no processing here 
            // all validation should be done here
            
            base.ProcessRecord();
        }
    }
}
