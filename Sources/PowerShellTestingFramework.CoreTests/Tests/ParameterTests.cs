﻿using PowerShellTestingFramework.Components;
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
            SkipHostOutput = true;
        }

        #region Dynamic Parameter
        
        [Fact]
        public void WatchRuntimeDefinedDictionary()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType RuntimeDefinedDictionary -Age 20 -Year 2010

                        ";

            var result = RunScript(script);

            Write(result);

        }

        [Fact]
        public void WatchRuntimeDefinedParameter()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType RuntimeDefinedParameter -Age 20

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.NotNull(result.Errors.FirstOrDefault(err => err.CategoryInfo is { Category: ErrorCategory.InvalidArgument,
                                                                                 Activity: "Watch-DynamicParameter",
                                                                                 Reason: nameof(ParameterBindingException) }));

        }

        [Fact]
        public void WatchDefinedClass()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType DefinedClass -Age 20

                        ";

            var result = RunScript(script);

            Write(result);
        }

        [Fact]
        public void WatchDefinedClassWithParameters()
        {
            var script = $@"

                        Watch-DynamicParameter -DynamicType DefinedClassWithParameters -Age 20 -Year 1990

                        ";

            var result = RunScript(script);

            Write(result);
        }

        #endregion

        #region Parameter validation

        [Fact]
        public void ValidateStringLengthFailing()
        {
            var script = $@"

                        Test-Parameters -Name 'Max'

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.NotNull(result.Errors.FirstOrDefault(err => err.Contains<ValidationMetadataException>()));
        }

        [Fact]
        public void ValidateStringLength()
        {
            var script = $@"

                        Test-Parameters -Name 'Maximilian'

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.False(result.Errors?.Any() ?? false);
        }

        #endregion


    }
}
