using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using PowerShellSampleComponents.Cmdlets;
using PowerShellSampleComponents.Provider;
using PowerShellTestingFramework.Components;
using Xunit;
using Xunit.Abstractions;

namespace PowerShellSampleComponents.Tests.Tests
{
    public class XmlFileProviderTests : PowerShellTestBase
    {
        public XmlFileProviderTests(ITestOutputHelper output) : base(output.WriteLine, typeof(AddContactCmdlet).Assembly)
        {

        }

        [Fact]
        public void Test1()
        {
            var script = $@"

                        New-Psdrive -Name 'Xml' -PSProvider XmlFile -Root 'D:\test'

                        ";

            var result = RunScript(script);

            Write(result);

            var driveinfo = result.Output.OfType<XmlFileDriveInfo>().FirstOrDefault();

            Assert.NotNull(driveinfo);
        }

        [Fact]
        public void Test2()
        {
            var filename = this.GetFileContent("TestData\\CommonData.xml");
             
            Assert.True(File.Exists(filename));
        }
    }
}
