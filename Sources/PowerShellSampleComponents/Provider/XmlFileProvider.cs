using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellSampleComponents.Provider
{
    [CmdletProvider("XmlFile", ProviderCapabilities.None)]
    public class XmlFileProvider : DriveCmdletProvider
    {
        protected override PSDriveInfo NewDrive(PSDriveInfo drive)
        {
            if (drive == null)
            {
                WriteError(new ErrorRecord(
                    new ArgumentNullException("drive"),
                    "NullDrive",
                    ErrorCategory.InvalidArgument,
                    null));

                return null;
            }

            return new XmlFileDriveInfo(drive, drive.Root);
        }

        protected override PSDriveInfo RemoveDrive(PSDriveInfo drive)
        {
            return base.RemoveDrive(drive);
        }

        
    }

    public class XmlFileDriveInfo : PSDriveInfo
    {
        private string _filename;

        internal XmlFileDriveInfo(PSDriveInfo driveInfo, string fileName)
            : base(driveInfo)
        {
            _filename = fileName;
        }
    }
}
