using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PowerShellTestingFramework.Components
{
    [Serializable]
    public class ScriptExecuter : MarshalByRefObject
    {
        #region Properties
        
        public List<Assembly> Assemblies { get; set; }

        public List<string> Modules { get; set; }
        
        public Dictionary<string, object> Variables { get; set; }

        public HostCommunicationAdapter CommunicationAdapter { get; set; } = null;

        public bool AutoInspectVariables { get; set; } = true;

        #endregion

        public ExecutionResult Execute(string script)
        {
            ExecutionResult runscriptresult = new ExecutionResult(script);

            using (PowerShell ps = PowerShell.Create())
            using (Runspace rs = RunspaceFactory.CreateRunspace(new TestEnvironmentHost(CommunicationAdapter, runscriptresult.AddHostOutput)))
            {
                rs.Open();

                // Übertragen bestehender Variablen und deren Werte
                Variables?.Keys.ToList().ForEach(variablename =>
                {
                    rs.SessionStateProxy.SetVariable(variablename, Variables[variablename]);
                });

                rs.StateChanged += delegate(object sender, RunspaceStateEventArgs args)
                {
                    
                };

                if (AutoInspectVariables)
                {
                    var pattern = $@"\$\b(?<item>\w+)\b";

                    Regex regex = new Regex(pattern);

                    var matches = regex.Matches(script);

                    if (matches.Count > 0)
                    {
                        if (Variables == null)
                            Variables = new Dictionary<string, object>();

                        foreach (Match m in matches)
                        {
                            string key = m.Value.Substring(1);
                            if (!Variables.ContainsKey(key))
                                Variables.Add(key, null);
                        }
                    }
                }
               
                // Modifizierte Ausführungsumgebung hinzufügen
                ps.Runspace = rs;

                // Hinzufügen der zusätzlichen Assemblies
                Assemblies?.ForEach(assembly =>
                {
                    ps.AddCommand("Import-Module")
                        .AddArgument(new Uri(assembly.CodeBase).LocalPath);
                });

                // Hinzufügen zusätzlicher Module
                Modules?.ForEach(module =>
                {
                    ps.AddCommand("Import-Module")
                        .AddArgument(module);
                });

                

                // Das eigentliche Skript hinzufügen
                ps.AddScript(script);

                var result = ps.Invoke();

                // Überführung des Ergebnisses
                runscriptresult.Errors.AddRange(ps.Streams.Error);
                runscriptresult.Warnings.AddRange(ps.Streams.Warning);
                runscriptresult.Debugs.AddRange(ps.Streams.Debug);
                runscriptresult.Verboses.AddRange(ps.Streams.Verbose.Select(v => v.Message));
                runscriptresult.Informations.AddRange(ps.Streams.Information);
                
                runscriptresult.Output.AddRange(result.Where(p => p != null && p.BaseObject != null)
                    .Select(p => p.BaseObject));

                // Auslesen der Variablen
                if (Variables != null)
                {
                    runscriptresult.Variables = new Dictionary<string, object>(Variables);

                    runscriptresult.Variables.Keys.ToList().ForEach(variablename
                        => runscriptresult.Variables[variablename] = rs.SessionStateProxy.GetVariable(variablename));
                }

                rs.Close();

            }

            return runscriptresult;
        }
    }
}
