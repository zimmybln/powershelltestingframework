using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Core.Tests.Provider
{

    // https://docs.microsoft.com/en-us/powershell/scripting/developer/provider/windows-powershell-provider-quickstart?view=powershell-7.1

    [CmdletProvider("Custom", ProviderCapabilities.None)]
    public class CustomStorageProvider : NavigationCmdletProvider
    {
        public CustomStorageProvider()
        {

        }

        protected override bool IsValidPath(string path)
        {
            return true;
        }

        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            return new CustomDriveInfo(drive);
        }

        protected override ProviderInfo Start(ProviderInfo providerInfo)
        {
            //var p = new CustomProviderInfo(providerInfo);

            //return p;

            return providerInfo;
        }

        //protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        //{
        //    var list = new List<CustomDriveInfo>()
        //    {
        //        new CustomDriveInfo(new PSDriveInfo("Custom", new ProviderInfo))
        //    };

        //    return new Collection<PSDriveInfo>(list.ToArray());
        //}
    }

    public class CustomDriveInfo : PSDriveInfo
    {
        public CustomDriveInfo(PSDriveInfo driveInfo)
            : base(driveInfo)
        {
            
        }
    }
}
