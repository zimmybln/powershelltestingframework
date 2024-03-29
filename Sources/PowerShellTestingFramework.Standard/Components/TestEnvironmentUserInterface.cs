﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;

namespace PowerShellTestingFramework.Components
{
    public class TestEnvironmentUserInterface : PSHostUserInterface
    {
        private readonly HostCommunicationAdapter _communicationAdapter;
        private readonly Action<string> _hostOutput;

        internal TestEnvironmentUserInterface(HostCommunicationAdapter communicationAdapter, Action<string> hostOutput)
        {
            _communicationAdapter = communicationAdapter ?? throw new ArgumentNullException(nameof(communicationAdapter));
            _hostOutput = hostOutput;
        }

        public override PSHostRawUserInterface RawUI => null;

        public override Dictionary<string, PSObject> Prompt(
                                  string caption,
                                  string message,
                                  Collection<FieldDescription> descriptions)
        {
            Dictionary<string, PSObject> results =
                     new Dictionary<string, PSObject>();

            if (_communicationAdapter == null)
                return null;

            foreach (FieldDescription fd in descriptions)
            {
                _hostOutput?.Invoke(fd.Name);
                string userData = _communicationAdapter.PromptForValue(fd.Name);

                if (userData == null)
                {
                    return null;
                }

                results[fd.Name] = PSObject.AsPSObject(userData);
            }

            return results;
        }

        public override int PromptForChoice(
                                            string caption,
                                            string message,
                                            Collection<ChoiceDescription> choices,
                                            int defaultChoice)
        {
            return _communicationAdapter.PromptForChoice(caption, message, choices, defaultChoice);
        }

        public override PSCredential PromptForCredential(
                                                         string caption,
                                                         string message,
                                                         string userName,
                                                         string targetName)
        {
            return PromptForCredential(caption, message, userName, targetName, PSCredentialTypes.Default, PSCredentialUIOptions.None);
        }

        public override PSCredential PromptForCredential(
                                           string caption,
                                           string message,
                                           string userName,
                                           string targetName,
                                           PSCredentialTypes allowedCredentialTypes,
                                           PSCredentialUIOptions options)
        {
            var password = _communicationAdapter.OnPromptForPassword(message, targetName, userName);

            SecureString secureString = new SecureString();

            foreach (char c in password)
            {
                secureString.AppendChar(c);
            }

            secureString.MakeReadOnly();

            return new PSCredential(userName, secureString);
        }

        public override string ReadLine()
        {
            return "Das ist ein weiterer Test";
        }

        public override SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException();
        }

        public override void Write(string value)
        {
            _hostOutput?.Invoke(value);
        }

        public override void Write(
                                   ConsoleColor foregroundColor,
                                   ConsoleColor backgroundColor,
                                   string value)
        {
            _hostOutput?.Invoke(value);

        }

        public override void WriteLine(
                                       ConsoleColor foregroundColor,
                                       ConsoleColor backgroundColor,
                                       string value)
        {
            _hostOutput?.Invoke(value);
        }


        public override void WriteLine()
        {

        }

        public override void WriteLine(string value)
        {
            _hostOutput?.Invoke(value);
        }

        public override void WriteInformation(InformationRecord record)
        {
            _hostOutput?.Invoke($"INFORMATION: {record.MessageData} (Tags: {string.Join(",", record.Tags)})");
        }

        public override void WriteDebugLine(string message)
        {
            _hostOutput?.Invoke($"DEBUG: {message}");
        }

        public override void WriteVerboseLine(string message)
        {
            _hostOutput?.Invoke($"VERBOSE: {message}");
        }

        public override void WriteWarningLine(string message)
        {
            _hostOutput?.Invoke($"WARNING: {message}");
        }

        public override void WriteErrorLine(string value)
        {
            _hostOutput?.Invoke($"ERROR: {value}");
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {

        }
    }
}
