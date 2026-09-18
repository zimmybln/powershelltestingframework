using System.Linq;
using System.Management.Automation;
using PowerShellTestingFramework.Components;
using PowerShellTestingFramework.Tests.Cmdlets;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellTestingFramework.Tests.Tests
{
    public class ParameterSetTests : PowerShellTestBase
    {
        public ParameterSetTests(ITestOutputHelper output) : base(output.WriteLine, typeof(GetPersonByParameterSetCommand).Assembly)
        {
            SkipHostOutput = true;
        }

        #region Parameter sets

        [Fact]
        public void UsesDefaultParameterSetByPosition()
        {
            var script = $@"

                        Get-PersonByParameterSet 5

                        ";

            var result = RunScript(script);

            Write(result);

            var person = result.Output.OfType<PersonResult>().FirstOrDefault();

            Assert.False(result.Errors?.Any() ?? false);
            Assert.NotNull(person);
            Assert.Equal("ById", person.ParameterSetName);
            Assert.Equal(5, person.Id);
        }

        [Fact]
        public void UsesByNameParameterSet()
        {
            var script = $@"

                        Get-PersonByParameterSet -Name 'Mustermann'

                        ";

            var result = RunScript(script);

            Write(result);

            var person = result.Output.OfType<PersonResult>().FirstOrDefault();

            Assert.False(result.Errors?.Any() ?? false);
            Assert.NotNull(person);
            Assert.Equal("ByName", person.ParameterSetName);
            Assert.Equal("Mustermann", person.Name);
        }

        [Fact]
        public void AmbiguousParameterSetFails()
        {
            var script = $@"

                        Get-PersonByParameterSet -Id 5 -Name 'Mustermann'

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.NotNull(result.Errors.FirstOrDefault(err => err.Contains<ParameterBindingException>()));
        }

        #endregion

        #region Pipeline binding

        [Fact]
        public void BindsIdFromPipelineByValue()
        {
            var script = $@"

                        5, 6 | Get-PersonByParameterSet

                        ";

            var result = RunScript(script);

            Write(result);

            var persons = result.Output.OfType<PersonResult>().ToList();

            Assert.False(result.Errors?.Any() ?? false);
            Assert.Equal(2, persons.Count);
            Assert.Contains(persons, p => p.Id == 5);
            Assert.Contains(persons, p => p.Id == 6);
        }

        [Fact]
        public void BindsIdFromPipelineByPropertyName()
        {
            var script = $@"

                        [PSCustomObject]@{{ Id = 7 }} | Get-PersonByParameterSet

                        ";

            var result = RunScript(script);

            Write(result);

            var person = result.Output.OfType<PersonResult>().FirstOrDefault();

            Assert.False(result.Errors?.Any() ?? false);
            Assert.NotNull(person);
            Assert.Equal(7, person.Id);
        }

        [Fact]
        public void BindsNameFromPipelineByPropertyName()
        {
            var script = $@"

                        [PSCustomObject]@{{ Name = 'Schmidt' }} | Get-PersonByParameterSet

                        ";

            var result = RunScript(script);

            Write(result);

            var person = result.Output.OfType<PersonResult>().FirstOrDefault();

            Assert.False(result.Errors?.Any() ?? false);
            Assert.NotNull(person);
            Assert.Equal("Schmidt", person.Name);
        }

        #endregion
    }
}
