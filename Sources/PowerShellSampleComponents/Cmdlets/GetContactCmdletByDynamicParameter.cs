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
    //public class GetContactCmdletByDynamicParameter : PSCmdlet
    //{
    //    [Parameter(Position = 0)]
    //    [ValidateNotNull]
    //    public Contact Contact { get; set; }

    //    protected override void ProcessRecord()
    //    {
    //        Contact contact = null;

    //        using (ContactsEntities model = new ContactsEntities())
    //        {
    //            if (Contact.Id > 0)
    //            {
    //                contact = model.Contacts.Find(Contact.Id);
    //            }
    //            else if (!String.IsNullOrWhiteSpace(Contact.SearchValue))
    //            {
    //                contact = model.Contacts
    //                    .FirstOrDefault(x => SqlFunctions.PatIndex(Contact.SearchValue, x.LastName) > 0);
    //            }
    //        }

    //        WriteObject(contact);
    //    }
    //}

}
