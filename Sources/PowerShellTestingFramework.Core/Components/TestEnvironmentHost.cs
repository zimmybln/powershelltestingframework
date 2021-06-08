using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation.Host;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Components
{
    internal class TestEnvironmentHost : PSHost
    {
        private CultureInfo originalUICultureInfo =
            System.Threading.Thread.CurrentThread.CurrentUICulture;

        private static Guid instanceId = Guid.NewGuid();

        public TestEnvironmentHost(HostCommunicationAdapter communicationAdapter, Action<string> hostOutput)
        {
           _testEnvironmentUserInterface = new TestEnvironmentUserInterface(communicationAdapter, hostOutput);
        }

        private readonly TestEnvironmentUserInterface _testEnvironmentUserInterface ;

        public override CultureInfo CurrentCulture { get; } = System.Threading.Thread.CurrentThread.CurrentCulture;

        public override CultureInfo CurrentUICulture
        {
            get { return this.originalUICultureInfo; }
        }

        public override Guid InstanceId
        {
            get { return instanceId; }
        }

        public override string Name
        {
            get { return this.GetType().Name; }
        }

        public override PSHostUserInterface UI
        {
            get { return this._testEnvironmentUserInterface; }
        }

        public override Version Version
        {
            get { return new Version(1, 0, 0, 0); }
        }

        public override void EnterNestedPrompt()
        {
            throw new NotImplementedException();
        }

        public override void ExitNestedPrompt()
        {
            throw new NotImplementedException();
        }

        public override void NotifyBeginApplication()
        {
            return;
        }

        public override void NotifyEndApplication()
        {
            return;
        }

        public override void SetShouldExit(int exitCode)
        {

        }
    }
}
