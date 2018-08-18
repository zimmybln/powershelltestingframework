using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PowerShellTestingFramework.Components
{
    [Serializable]
    public class ExecutionResult
    {
        private readonly List<string> _hostOutput = new List<string>();


        public ExecutionResult()
        {

        }

        public ExecutionResult(string script)
        {
            Script = script;
        }

        public string Script { get; }

        public IReadOnlyList<string> HostOutput => _hostOutput.AsReadOnly();
        
        public List<ErrorRecord> Errors { get; } = new List<ErrorRecord>();

        public List<WarningRecord> Warnings { get; } = new List<WarningRecord>();

        public List<DebugRecord> Debugs { get; } = new List<DebugRecord>();

        public List<string> Verboses { get; } = new List<string>();

        public List<InformationRecord> Informations { get; } = new List<InformationRecord>();

        public List<Object> Output { get; } = new List<object>();
        
        public Dictionary<string, object> Variables { get; set; }

        internal void AddHostOutput(string value)
        {
            _hostOutput.Add(value);
        }
    }
}
