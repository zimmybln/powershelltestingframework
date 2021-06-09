using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Core.Tests.Cmdlets
{
    [Cmdlet(VerbsDiagnostic.Test, "Input")]
    public class TestInput : PSCmdlet
    {
        protected override void ProcessRecord()
        {
            var fields = new System.Collections.ObjectModel.Collection<System.Management.Automation.Host.FieldDescription>();

            fields.Add(new System.Management.Automation.Host.FieldDescription("Name"));

            var input = Host.UI.Prompt("Test", "Bitte gebe Test ein", fields);

            WriteObject(input);
        }
    }
}
