# PowerShell Testing Framework

The PowerShell Testing Framework enables to develop PowerShell cmdlets test driven. It contains various tools, base class etc. to execute PowerShell scripts withing your Visual Studio combined with your favorite testing tool. You may use within your script common commands and your custom cmdlets and you have the opportunity to inspect nearly all kinds of output, variables and results.

### Sample
This sample uses XUnit as testing execution framework. The test `AddContactWithExplicitParameter` executes the script and checks if any errors occures. Finally it checks some assumtions about the result of the execution. The testfixture adds the module containing the custom cmdlet `Add-Contact`.

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

                Write(result); // show the full output, including Debug, Warnings, Errors etc.

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
