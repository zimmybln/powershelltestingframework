# PowerShell Testing Framework

The PowerShell Testing Framework enables to develop PowerShell cmdlets test driven. It contains various tools, base class etc. to execute a PowerShell script withing your Visual Studio and your favorite testing tool.

### Sample
This sample uses XUnit as testing execution framework. The test `AddContactWithExplicitParameter` executes the script and checks if any errors occures. Finally it checks some assumtions about the result of the execution.

        public class AddContactTests : PowerShellTestBase
        {
            public AddContactTests(ITestOutputHelper output) : base(output.WriteLine, typeof(AddContactCmdlet).Assembly)
            {

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

                Assert.False(result.Errors.Any());
                Assert.NotNull(contact);
                Assert.True(contact.Id > 0);
                Assert.Equal("Martina", contact.FirstName);
                Assert.Equal("Mustermann", contact.LastName);

            }
        }



### NuGet

You'll find the binaries at nuget here:

    Install-Package Zimmy.PowerShell.Testing.Framework -Version 1.0.0

It is the first release without any documentation except a lot of test and samples.
