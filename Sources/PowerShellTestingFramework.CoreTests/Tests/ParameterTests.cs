using PowerShellTestingFramework.Components;
using PowerShellTestingFramework.Test.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using PowerShellTestingFramework.Tests.Cmdlets;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellTestingFramework.Tests.Tests
{
    public class ParameterTests : PowerShellTestBase
    {
        public ParameterTests(ITestOutputHelper output) : base(output.WriteLine, typeof(HostCommunicationTests).Assembly)
        {

        }

        #region Dynamic Parameters

        [Fact]
        public void UseDynamicParametersAsRuntimeDictionary()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType RuntimeDefinedDictionary -Age 20 -Year 2010

                        ";

            var result = RunScript(script);

            Write(result);

        }

        [Fact]
        public void UseDynamicParametersAsSingleRuntimeParameter()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType RuntimeDefinedParameter -Age 20

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Errors.Any(err => err.CategoryInfo is { Category: ErrorCategory.InvalidArgument,
                                                                                 Activity: "Watch-DynamicParameter",
                                                                                 Reason: nameof(ParameterBindingException) }));

        }

        [Fact]
        public void UseDynamicParametersAsPredefinedClassWithOneParameter()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType DefinedClass -Age 20

                        ";

            var result = RunScript(script);

            Write(result);
        }

        [Fact]
        public void UseDynamicParametersAsPredefinedClassWithMoreParameters()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType DefinedClassWithParameters -Age 20 -Year 1990

                        ";

            var result = RunScript(script);

            Write(result);
        }

        #endregion
    }
}
