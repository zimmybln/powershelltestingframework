using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Core.Tests.Provider
{
    public class CustomProviderInfo : ProviderInfo
    {
        public CustomProviderInfo(ProviderInfo providerInfo) : base(providerInfo)
        {
            this.Home = "Custom";

            this.Description = "Ein Provider für Tests";

            var root = new PSDriveInfo("NC", this, "NoventCare", "Basisverzeichnis", null);

            this.Drives.Add(root);
        }
    }
}
