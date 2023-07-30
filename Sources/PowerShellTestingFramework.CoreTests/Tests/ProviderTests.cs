using PowerShellTestingFramework.Components;
using PowerShellTestingFramework.Test.Tests;
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
        public void UseVirtualSpaceProvider()
        {
            var script = $@"

                    Set-Location 'VirtualSpace'

                    ";

            var result = RunScript(script);

            Write(result);
        }

    }
}
