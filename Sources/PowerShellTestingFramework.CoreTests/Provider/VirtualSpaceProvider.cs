using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Tests.Provider
{
    [CmdletProvider("VirtualSpace", ProviderCapabilities.None)]
    public class VirtualSpaceProvider : DriveCmdletProvider
    {
        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
            return base.Start(providerInfo);
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            return base.NewDrive(drive);
        }
    }
}
