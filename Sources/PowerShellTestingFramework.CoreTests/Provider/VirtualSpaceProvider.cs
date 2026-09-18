using System;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PowerShellTestingFramework.Tests.Provider
{
    [CmdletProvider("VirtualSpace", ProviderCapabilities.None)]
    public class VirtualSpaceProvider : DriveCmdletProvider
    {
        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                    new ArgumentNullException(nameof(drive)),
                    "NullDrive",
                    ErrorCategory.InvalidArgument,
                    null));

                return null;
            }

            return new VirtualSpaceDriveInfo(drive);
        }
    }

    public class VirtualSpaceDriveInfo : PSDriveInfo
    {
        internal VirtualSpaceDriveInfo(PSDriveInfo driveInfo)
            : base(driveInfo)
        {
        }
    }
}
