using NUnit.Framework;
using PowerShellSampleComponents.Tests.Cmdlets;
using PowerShellTestingFramework.Components;
using System;

namespace PowerShellTestingFramework.Test.Tests
{
    public class LifecycleTests : PowerShellTestBase
    {
        public LifecycleTests() : base(Console.WriteLine, typeof(ShowLifecycleCmdlet).Assembly)
        {

        }

        [Test]
        public void RunLifecycleWithoutPipe()
        {
            var script = @"
               
                        Show-Lifecycle 'Case Zero' -Debug

                        ";

            var result = RunScript(script);

            Write(result);
        }

        [Test]
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
