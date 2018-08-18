using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using PowerShellSampleComponents.Cmdlets;
using PowerShellSampleComponents.Data;
using PowerShellTestingFramework.Components;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellSampleComponents.Tests
{
    public class AddContactTests : PowerShellTestBase
    {
        public AddContactTests(ITestOutputHelper output) : base(output.WriteLine, typeof(AddContactCmdlet).Assembly)
        {

        }

        [Fact]
        public void Test1()
        {
            var assemblyfile = new Uri(typeof(AddContactCmdlet).Assembly.CodeBase).LocalPath;
            
            var script = $@"

                        Import-Module {assemblyfile}

                        Add-Contact -FirstName 'Martina' -LastName 'Mustermann'

                        ";

            using (PowerShell powerShell = PowerShell.Create())
            {
                powerShell.AddScript(script);

                powerShell.Invoke();

            }
            
        }

        [Fact]
        public void AddContactWithExplicitParameter()
        {
            var script = $@"

                        Add-Contact -FirstName 'Martina' -LastName 'Mustermann'

                        ";

            var result = RunScript(script);

            Write(result);

            var contact = result.Output.OfType<Contact>().FirstOrDefault();

            Assert.NotNull(contact);
            Assert.True(contact.Id > 0);
            Assert.Equal("Martina", contact.FirstName);
            Assert.Equal("Mustermann", contact.LastName);

        }
        
        [Fact]
        public void AddContactWithParametersByPosition()
        {
            var script = $@"

                        Add-Contact 'Martina' 'Mustermann'

                        ";

            var result = RunScript(script);

            Write(result);

        }

        [Fact]
        public void AddContactWithLastNameByPosition()
        {
            var script = $@"

                        Add-Contact 'Unternehmen 1'

                        ";

            var result = RunScript(script);

            Write(result);

        }
    }
}
