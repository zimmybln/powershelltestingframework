using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Text;
using System.Threading.Tasks;
using PowerShellSampleComponents.Data;

namespace PowerShellSampleComponents.Cmdlets
{
    [Cmdlet(VerbsCommon.Add, "Contact", DefaultParameterSetName = "LastNameOnly")]
    public class AddContactCmdlet : PSCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "Default", ValueFromPipelineByPropertyName = true)]
        public string FirstName { get; set; }

        [Parameter(Position = 1, ParameterSetName = "Default")]
        [Parameter(Position = 0, Mandatory = true, ParameterSetName = "LastNameOnly")]
        public string LastName { get; set; }

        protected override void BeginProcessing()
        {
            
        }

        protected override void ProcessRecord()
        {
            if (String.IsNullOrWhiteSpace(LastName))
                throw new ArgumentNullException(nameof(LastName));
            
            // Auswertung der Parameter
            var contact = new Contact
            {
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            

            try
            { 
                WriteDebug("Datenbankverbindung wird geöffnet");

                using (ContactsEntities model = new ContactsEntities())
                {
                    WriteDebug("Datenbankverbindung geöffnet");

                    model.Contacts.Add(contact);

                    model.SaveChanges();

                    WriteDebug("Schließe Datenbankverbindung");
                }

                WriteObject(contact);
            }
            catch (Exception e)
            {
                // Auswertung des Fehlers...
                WriteError(new ErrorRecord(e, "1", ErrorCategory.WriteError, null));
            }
            
        }
    }
}
