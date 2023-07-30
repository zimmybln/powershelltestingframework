using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Test.Cmdlets
{
    [Cmdlet(VerbsCommon.Show, "UserChoice")]
    [OutputType(typeof(int))]
    public class ShowUserChoiceCommand : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string[] Choices { get; set; }

        [Parameter] public int DefaultChoice { get; set; } = 0;


        protected override void ProcessRecord()
        {
            var choices = new Collection<ChoiceDescription>();

            foreach (string choice in Choices)
            {
                choices.Add(new ChoiceDescription(choice));
            }

            var selectedchoice = this.Host.UI.PromptForChoice(nameof(ShowUserChoiceCommand), "Select a color", choices, DefaultChoice);

            WriteObject(selectedchoice);
            
        }
    }
}
