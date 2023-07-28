using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellSampleComponents.Cmdlets
{
    //[Cmdlet(VerbsCommon.Get, "Contact")]
    //[OutputType(typeof(Contact))]
    //public class GetContactCmdlet : PSCmdlet
    //{
    //    [Parameter(Position = 0)]
    //    public int Id { get; set; }

    //    protected override void ProcessRecord()
    //    {
    //        List<Contact> contacts = new List<Contact>();

    //        using (ContactsEntities model = new ContactsEntities())
    //        {
    //            if (Id > 0)
    //            {
    //                var contact = model.Contacts.Find(Id);

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
