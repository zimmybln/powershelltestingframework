using System.Management.Automation;

namespace PowerShellTestingFramework.Tests.Cmdlets
{
    public class PersonResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ParameterSetName { get; set; }
    }

    [Cmdlet(VerbsCommon.Get, "PersonByParameterSet", DefaultParameterSetName = "ById")]
    [OutputType(typeof(PersonResult))]
    public class GetPersonByParameterSetCommand : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ById",
            ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public int Id { get; set; }

        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "ByName",
            ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject(new PersonResult
            {
                Id = Id,
                Name = Name,
                ParameterSetName = ParameterSetName
            });
        }
    }
}
