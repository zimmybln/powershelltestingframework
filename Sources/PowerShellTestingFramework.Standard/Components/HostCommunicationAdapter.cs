using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation.Host;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Components
{

    public delegate string PromptForPasswordHandler(string message, string targetName, string userName);

    public class HostCommunicationAdapter
    {
        public Func<string, string> OnPromptForValue { get; set; }

        public PromptForPasswordHandler OnPromptForPassword { get; set; }

        public Func<string, string, Collection<ChoiceDescription>, int, int> OnPromptForChoice { get; set; }


        public virtual string PromptForValue(string message)
        {
            return OnPromptForValue?.Invoke(message);
        }

        public virtual int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices,
            int defaultChoice)
        {
            return OnPromptForChoice?.Invoke(caption, message, choices, defaultChoice) ?? defaultChoice;
        }

        public virtual string PromptForCredentials(string message, string targetName, string userName)
        {
            return OnPromptForPassword?.Invoke(message, targetName, userName) ?? null;
        }
    }
}
