using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PowerShellTestingFramework.Components;


namespace PowerShellTestingFramework.Test.Tests
{
    public class UsingVariablesTests : PowerShellTestBase
    {
        public UsingVariablesTests() : base(Console.WriteLine)
        {
        }

        [Test]
        public void AccessVariablesOnOutput()
        {
            var script = @"

                        $test = 'Hallo'

                    ";

            var variables = new Dictionary<string, object> { { "test", null } };

            var result = RunScript(script, variables: variables);

            Assert.True(result.Variables.ContainsKey("test"));
            Assert.AreEqual("Hallo", result.Variables["test"]);
        }

        [Test]
        public void AccessVariableOnInput()
        {
            var value = "Some value test";


            var script = @"

                        Write-Output $test

                    ";

            var variables = new Dictionary<string, object> { { "test", value } };

            var result = RunScript(script, variables: variables);
            var output = result.Output.OfType<string>().FirstOrDefault();

            Assert.AreEqual(value, output);
        }
    }
}
