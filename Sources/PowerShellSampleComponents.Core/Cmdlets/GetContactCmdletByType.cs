using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using PowerShellSampleComponents.Data;

namespace PowerShellSampleComponents.Cmdlets
{
    //[Cmdlet(VerbsCommon.Get, "Contact")]
    //[OutputType(typeof(Contact))]
    //public class GetContactCmdletByType : PSCmdlet
    //{
    //    [Parameter(Position = 0)]
    //    public Contact Contact { get; set; }

    //    protected override void ProcessRecord()
    //    {
    //        List<Contact> contacts = new List<Contact>();

    //        using (ContactsEntities model = new ContactsEntities())
    //        {
    //            if (Contact != null && Contact.Id > 0)
    //            {
    //                var contact = model.Contacts.Find(Contact.Id);

    //                if (contact != null)
    //                {
    //                    contacts.Add(contact);
    //                }
    //            }
    //            else if (!String.IsNullOrWhiteSpace(Contact?.SearchValue))
    //            {
    //                var contact = model.Contacts
    //                    .FirstOrDefault(x => SqlFunctions.PatIndex(Contact.SearchValue, x.LastName) > 0);

    //                if (contact != null)
    //                {
    //                    contacts.Add(contact);
    //                }
    //            }
    //            else
    //            {
    //                contacts.AddRange(model.Contacts.ToList());
    //            }
    //        }

    //        WriteObject(contacts, true);
    //    }
    //}

}
