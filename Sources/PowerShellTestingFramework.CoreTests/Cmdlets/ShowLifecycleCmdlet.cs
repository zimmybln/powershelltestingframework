using System.Management.Automation;

namespace PowerShellSampleComponents.Tests.Cmdlets
{
    [Cmdlet(VerbsCommon.Show, "Lifecycle")]
    public class ShowLifecycleCmdlet : PSCmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public string Name { get; set; }

        protected override void BeginProcessing()
        {
            WriteDebug("BeginProcessing " + Name?.ToString());
        }

        protected override void ProcessRecord()
        {
            WriteDebug($"ProcessRecord: {Name}");
        }

        protected override void EndProcessing()
        {
            WriteDebug("EndProcessing" + Name);
        }
    }
}
