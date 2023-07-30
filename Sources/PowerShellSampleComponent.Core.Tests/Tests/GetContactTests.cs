using System;
using System.Linq;
using PowerShellSampleComponents.Cmdlets;
using PowerShellSampleComponents.Data;
using PowerShellTestingFramework.Components;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellSampleComponents.Tests.Tests
{
    public class GetContactTests : PowerShellTestBase
    {
        private readonly string _assemblyname = new Uri(typeof(GetContactCmdletByParameterSet).Assembly.CodeBase).LocalPath;

        public GetContactTests(ITestOutputHelper output) : base(output.WriteLine)
        {
        }

        [Fact]
        public void GetContactWithoutRestriction()
        {
            // Testvorbereitung
            var contact = new Contact() { FirstName = "Dieter", LastName = "Müller" };

            using (ContactsEntities model = new ContactsEntities())
            {
                model.Contacts.Add(contact);

                model.SaveChanges();
            }

            int id = contact.Id;

            // Durchführung

            var script = $@"Import-Module '{_assemblyname}'

                        Get-Contact

                        ";

            var result = RunScript(script);

            Write(result);

            var contacts = result.Output.OfType<Contact>();

            Assert.NotNull(contacts);
            Assert.True(contacts.Any());
            Assert.DoesNotContain(contacts, c => c.Id == 0);
            Assert.Contains(contacts, c => c.LastName.Equals("Müller"));
        }

        [Fact]
        public void GetContactById()
        {
            // Testvorbereitung
            var contact = new Contact() {FirstName = "Hans", LastName = "Mustermann"};

            using (ContactsEntities model = new ContactsEntities())
            {
                model.Contacts.Add(contact);

                model.SaveChanges();
            }

            int id = contact.Id;
            
            // Durchführung

            var script = $@"Import-Module '{_assemblyname}'

                        Get-Contact {id}

                        ";

            var result = RunScript(script);

            Write(result);

            var contactresult = result.Output.OfType<Contact>().FirstOrDefault();

            Assert.NotNull(contactresult);
            Assert.True(contactresult.Id > 0);
            Assert.Equal("Hans", contactresult.FirstName);
            Assert.Equal("Mustermann", contactresult.LastName);
        }

        [Fact]
        public void GetContactByIdAndPipeline()
        {
            // Testvorbereitung
            var contactone = new Contact() { FirstName = "Hans", LastName = "Mustermann" };
            var contacttwo = new Contact() {FirstName = "Franz", LastName = "Heinrich"};
            
            using (ContactsEntities model = new ContactsEntities())
            {
                model.Contacts.Add(contactone);
                model.Contacts.Add(contacttwo);

                model.SaveChanges();
            }

            
            var script = $@"Import-Module '{_assemblyname}'

                        {contactone.Id}, {contacttwo.Id} | Get-Contact | Export-Csv -Path 'D:\personen.txt'

                        ";

            var result = RunScript(script);

            Write(result);

            var contactresult = result.Output.OfType<Contact>();

            //Assert.NotNull(contactresult);
            //Assert.True(contactresult.Count() == 2);
            //Assert.Contains(contactresult, c => c.Id == contactone.Id);
            //Assert.Contains(contactresult, c => c.Id == contacttwo.Id);

        }

        [Fact]
        public void GetContactByName()
        {
            // Testvorbereitung
            var contact = new Contact() { FirstName = "Hans", LastName = "Schmidt" };

            using (ContactsEntities model = new ContactsEntities())
            {
                model.Contacts.Add(contact);

                model.SaveChanges();
            }

            int id = contact.Id;

            // Durchführung

            var script = $@"Import-Module '{_assemblyname}'

                        Get-Contact 'Schmidt'

                        ";

            var result = RunScript(script);

            Write(result);

            var contactresult = result.Output.OfType<Contact>().FirstOrDefault();

            Assert.NotNull(contactresult);
            Assert.True(contactresult.Id > 0);
            Assert.Equal("Hans", contactresult.FirstName);
            Assert.Equal("Schmidt", contactresult.LastName);
        }


    }
}
