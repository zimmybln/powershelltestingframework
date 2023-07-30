using PowerShellSampleComponents.Tests.Cmdlets;
using PowerShellTestingFramework.Components;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellTestingFramework.Test.Tests
{
    public class LifecycleTests : PowerShellTestBase
    {
        public LifecycleTests(ITestOutputHelper output) : base(output.WriteLine, typeof(ShowLifecycleCmdlet).Assembly)
        {

        }

        [Fact]
        public void RunLifecycleWithoutPipe()
        {
            var script = @"
               
                        Show-Lifecycle 'Case Zero' -Debug

                        ";

            var result = RunScript(script);

            Write(result);
        }

        [Fact]
        public void RunLifecycleWithPipe()
        {
            var script = @"
                
                        'Case One', 'Case Two' | Show-Lifecycle -Debug

                        ";

            var result = RunScript(script);

            Write(result);
        }
    }
}
