using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Core.Tests.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "Password")]
    public class PromptForPasswordCommand : PSCmdlet
    {

        public PromptForPasswordCommand()
        {

        }

        protected override void ProcessRecord()
        {
            
            PSCredential credential = this.Host.UI.PromptForCredential("asdf", "asdfasdf", "UserName", "TargetName");
                                    

            if (credential != null)
            {
                WriteObject(ConvertSecureStringToString(credential.Password));
            }


        }

        private static string ConvertSecureStringToString(SecureString data)
        {
            var pointer = IntPtr.Zero;
            try
            {
                pointer = Marshal.SecureStringToGlobalAllocUnicode(data);
                return Marshal.PtrToStringUni(pointer);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(pointer);
            }
        }



    }
}
