using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using NUnit.Framework;
using PowerShellTestingFramework.Components;

namespace PowerShellTestingFramework.Test.Tests
{
    public class HostCommunicationTests : PowerShellTestBase
    {
        public HostCommunicationTests() : base(Console.WriteLine, typeof(HostCommunicationTests).Assembly)
        {

        }

        [Test]
        public void WriteTextToHost()
        {
            var script = $@"

                        Write-Host 'Hello World'

                        ";

            var result = RunScript(script);

            Write(result);

            Assert.Contains("Hello World", (System.Collections.ICollection)result.HostOutput);
        }

        [Test]
        public void ReadTextFromHostByCustomCmdlet()
        {
            string fnPrompt(string message)
            {
                return "Hallo von hier";
            }

            var script = $@"

                        $input = Test-Input 

                        Write-Output $input

                        ";

            var result = RunScript(script, promptForValueFunc: fnPrompt);

            Write(result);
        }

        [Test]
        public void ReadTextFromHost()
        {
            const string enteryourname = "Bitte gib deinen Namen ein";

            string fnPrompt(string message)
            {
                switch (message)
                {
                    case enteryourname:
                        return "Martina";
                    default:
                        return null;
                }
            }

            var script = $@"

                        $name = Read-Host '{enteryourname}'
                        Write-Host $name
                        Write-Output $name
                        ";

            var result = RunScript(script, promptForValueFunc: fnPrompt);

            Write(result);

            Assert.AreEqual("Martina", result.Variables["name"].ToString());
            Assert.IsTrue(result.HostOutput.ToList().Contains("Martina"));
            Assert.IsTrue(result.Output.OfType<string>().Contains("Martina"));
        }

        [Test]
        public void RunInformationCommand()
        {
            var script = $@"
                    Show-TypesOfInformation -Debug -Verbose
                ";

            var result = RunScript(script);

            Write(result);
        }

        [Test]
        public void WriteError()
        {
            var errormessage = "This is an error message";

            var script = $@"
                        Write-Error -Message '{errormessage}'
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Errors.Count == 1);
            //Assert.Contains(result.Errors, err => err.Exception.Message.Equals(errormessage));
        }

        [Test]
        public void WriteVerboseWithoutParameter()
        {
            var verbosemessage = "This is a verbose message";

            var script = $@"
                        Write-Verbose '{verbosemessage}'
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.False(result.Verboses.Any());
            //Assert.DoesNotContain(result.Verboses, vb => vb.Equals(verbosemessage));

            //Assert.DoesNotContain(result.HostOutput, vb => vb.Contains(verbosemessage));
        }

        [Test]
        public void WriteVerbose()
        {
            var verbosemessage = "This is a verbose message";

            var script = $@"
                        Write-Verbose '{verbosemessage}' -Verbose
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Verboses.Count == 1);
            //Assert.Contains(result.Verboses, vb => vb.Equals(verbosemessage));

            //Assert.Contains(result.HostOutput, vb => vb.Contains(verbosemessage));
        }

        [Test]
        public void WriteDebugWithoutParameter()
        {
            var debugmessage = "This is a debug message";

            var script = $@"
                        Write-Debug '{debugmessage}'
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.False(result.Debugs.Any());
            //Assert.DoesNotContain(result.Debugs, db => db.Message.Equals(debugmessage));

            //Assert.DoesNotContain(result.HostOutput, db => db.Contains(debugmessage));
        }

        [Test]
        public void WriteDebug()
        {
            var debugmessage = "This is a debug message";

            var script = $@"
                        Write-Debug '{debugmessage}' -Debug
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Debugs.Count == 1);
            //Assert.Contains(result.Debugs, db => db.Message.Equals(debugmessage));

            //Assert.Contains(result.HostOutput, db => db.Contains(debugmessage));
        }

        [Test]
        public void WriteWarning()
        {
            var warningmessage = "This is a warning message";

            
            var script = $@"
                        Write-Warning '{warningmessage}'
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Warnings.Count == 1);
            //Assert.Contains(result.Warnings, w => w.Message.Contains(warningmessage));

            //Assert.Contains(result.HostOutput, w => w.Contains(warningmessage));
        }

        [Test]
        public void WriteWarningWithChoice()
        {
            var warningmessage = "This is a warning message";
            
            var script = $@"
                        Write-Warning '{warningmessage}' -WarningAction Inquire
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Warnings.Count == 1);
            //Assert.Contains(result.Warnings, w => w.Message.Contains(warningmessage));

            //Assert.Contains(result.HostOutput, w => w.Contains(warningmessage));
        }

        [Test]
        public void WriteInformation()
        {
            var informationmessage = "This is an information message";


            var script = $@"
                        Write-Information '{informationmessage}' -Tags 'eins', 'zwei'
                        ";

            var result = RunScript(script);

            Write(result);

            Assert.True(result.Informations.Count == 1);
            //Assert.Contains(result.Informations, i => i.MessageData.ToString().Contains(informationmessage));

            //Assert.Contains(result.HostOutput, i => i.Contains(informationmessage));
        }

        [Test]
        public void ReadAndWriteVariables()
        {
            var script = $@"

                    $one = 10
                    $two = 20
                    $three = $one + $two
                ";

            var result = RunScript(script);

            Assert.True(result.Variables.ContainsKey("one"));
            Assert.True(result.Variables.ContainsKey("two"));
            Assert.True(result.Variables.ContainsKey("three"));

            Assert.AreEqual(10, (int)result.Variables["one"]);
            Assert.AreEqual(20, (int)result.Variables["two"]);
            Assert.AreEqual(30, (int)result.Variables["three"]);
        }

        [Test]
        public void ReadAndWriteVariablesWithinString()
        {
            var script = $@"

                    $one = 10
                    $two = ""The value is $one""

                ";

            var result = RunScript(script);

            Assert.True(result.Variables.ContainsKey("one"));
            Assert.True(result.Variables.ContainsKey("two"));

            Assert.AreEqual(10, (int)result.Variables["one"]);
            Assert.AreEqual("The value is 10", result.Variables["two"]);
            
        }
    }
}
