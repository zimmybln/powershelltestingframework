using PowerShellTestingFramework.Components;
using PowerShellTestingFramework.Test.Tests;
using PowerShellTestingFramework.Tests.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellTestingFramework.Tests
{
    public class ProviderTests : PowerShellTestBase
    {
        public ProviderTests(ITestOutputHelper output) : base(output.WriteLine, typeof(HostCommunicationTests).Assembly)
        {

        }

        [Fact]
        public void GetItemFromDefaultProvider()
        {
            var script = $@"

                        Get-Location

                        Get-Item '*.*' | Format-Table -Property Length, Name

                        ";

            var result = RunScript(script);

            Write(result);
        }

        [Fact]
        public void CreateVirtualSpaceDrive()
        {
            var script = $@"

                        New-PSDrive -Name 'Virt' -PSProvider VirtualSpace -Root 'VirtualRoot'

                        ";

            var result = RunScript(script);

            Write(result);

            var driveInfo = result.Output.OfType<VirtualSpaceDriveInfo>().FirstOrDefault();

            Assert.False(result.Errors?.Any() ?? false);
            Assert.NotNull(driveInfo);
            Assert.Equal("VirtualRoot", driveInfo.Root);
        }

        [Fact]
        public void RemoveVirtualSpaceDrive()
        {
            var script = $@"

                        New-PSDrive -Name 'Virt' -PSProvider VirtualSpace -Root 'VirtualRoot' | Out-Null

                        Remove-PSDrive -Name 'Virt'

                        Get-PSDrive -Name 'Virt' -ErrorAction SilentlyContinue

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.False(result.Errors?.Any() ?? false);
            Assert.DoesNotContain(result.Output.OfType<VirtualSpaceDriveInfo>(), d => d.Name == "Virt");
        }

    }
}
