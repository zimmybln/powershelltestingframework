using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using PowerShellSampleComponents.Core.Data;

namespace PowerShellSampleComponents.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "Contact", DefaultParameterSetName = "SearchById")]
    [OutputType(typeof(Contact))]
    public class GetContactCmdletByParameterSet : PSCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "SearchById", ValueFromPipeline = true)]
        public int Id { get; set; }

        [Parameter(Position = 0, ParameterSetName = "SearchByPhrase", ValueFromPipeline = true)]
        public string SearchPhrase { get; set; }

        private ContactsEntities _contactsEntities = null;

        protected override void BeginProcessing()
        {
            _contactsEntities = new ContactsEntities();
        }

        protected override void EndProcessing()
        {
            _contactsEntities.Dispose();
        }

        protected override void ProcessRecord()
        {
            List<Contact> contacts = new List<Contact>();

            if (this.ParameterSetName.Equals("SearchById") && Id > 0)
            {
                var contact = _contactsEntities.Contacts.Find(Id);

                if (contact != null)
                {
                    contacts.Add(contact);
                }
            }
            else if (this.ParameterSetName.Equals("SearchByPhrase"))
            {
                contacts.AddRange(_contactsEntities.Contacts.Where(x => SqlFunctions.PatIndex(SearchPhrase, x.LastName) > 0).ToList());
            }
            else
            {
                contacts.AddRange(_contactsEntities.Contacts);
            }

            WriteObject(contacts, true);
        }
    }
}
