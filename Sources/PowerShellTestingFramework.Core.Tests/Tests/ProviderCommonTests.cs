using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerShellTestingFramework.Components;
using PowerShellSampleComponents.Tests.Cmdlets;

namespace PowerShellTestingFramework.Core.Tests
{


    [TestFixture]
    public class ProviderTests : PowerShellTestBase
    {
        public ProviderTests() : base(Console.WriteLine, typeof(ShowLifecycleCmdlet).Assembly)
        {

        }


        [Test]
        public void Test()
        {
            //var script = @"
               
            //            Get-PSProvider
            //            Set-Location -Path 'NC:'

            //            ";

            //var result = RunScript(script);

            //Write(result);
        }
    }
}
