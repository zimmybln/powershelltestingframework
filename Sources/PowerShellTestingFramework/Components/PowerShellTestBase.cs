using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace PowerShellTestingFramework.Components
{
    public abstract class PowerShellTestBase
    {
        private readonly Action<string> _outputAction;
        private readonly Assembly _assemblyToTest = null;

        protected PowerShellTestBase(Action<string> outputAction)
        {
            _outputAction = outputAction;
        }

        protected PowerShellTestBase(Action<string> outputAction, Assembly assemblyToTest)
        {
            _outputAction = outputAction;
            _assemblyToTest = assemblyToTest;
        }
        
        protected string GetFileFromDirectory(string filename)
        {
            string localpath = Path.GetDirectoryName(new Uri(this.GetType().Assembly.CodeBase).LocalPath);
            return Path.Combine(localpath, filename);
        }

        protected void WriteLine(string value)
        {
            _outputAction?.Invoke(value);
        }

        protected void Write(IEnumerable<PSObject> items)
        {
            foreach (PSObject item in items)
            {
                var obj = item.BaseObject;

                Write(obj);

                if (obj is String)
                    continue;

                var iteration = obj as IEnumerable;

                if (iteration != null)
                {
                    foreach (var i in iteration)
                    {
                        _outputAction?.Invoke($"{i.ToString()} (Type: {item.GetType().Name})");
                    }
                }
            }
        }

        protected void Write(ExecutionResult result, bool noItems = false)
        {
            if (!String.IsNullOrWhiteSpace(result.Script))
            {
                _outputAction?.Invoke(result.Script);
                _outputAction?.Invoke(String.Empty);
            }

            var hostoutput = result.HostOutput;

            if (hostoutput.Any())
            {
                _outputAction?.Invoke("Host");
                foreach (var line in hostoutput)
                {
                    _outputAction?.Invoke($"\t{line}");
                }
            }
            
            if (!noItems)
            {
                _outputAction?.Invoke("Output");
                result.Output.ForEach(o => Write(o));
            }

            if (result.Verboses.Any())
            {
                _outputAction?.Invoke("Verbose");
                result.Verboses.ForEach(Write);
            }

            if (result.Errors.Any())
            {
                _outputAction?.Invoke("Fehler:");
                result.Errors.ForEach(Write);
            }

            if (result.Warnings.Any())
            {
                _outputAction?.Invoke("Warnungen");
                result.Warnings.ForEach(Write);
            }

            if (result.Informations.Any())
            {
                _outputAction?.Invoke("Informationen");
                result.Informations.ForEach(Write);
            }

            if (result.Debugs.Any())
            {
                _outputAction?.Invoke("Debug");
                result.Debugs.ForEach(Write);
            }
        }

        protected void Write(ErrorRecord error)
        {
            if (error == null)
                return;

            if (error.ErrorDetails != null)
            {
                _outputAction?.Invoke($"\t{error.ErrorDetails.Message}");
            }

            if (error.Exception != null)
            {
                _outputAction?.Invoke($"\t{error.Exception.Message}");
            }

            if (!String.IsNullOrEmpty(error.ScriptStackTrace))
            {
                _outputAction?.Invoke($"\t{error.ScriptStackTrace}");
            }

            if (error.InvocationInfo != null && error.InvocationInfo.MyCommand != null)
            {
                _outputAction?.Invoke($"\t{error.InvocationInfo.MyCommand.Name}:");
            }

        }

        protected void Write(WarningRecord warning)
        {
            if (warning == null)
                return;

            _outputAction?.Invoke($"\t{warning.Message}");
        }

        protected void Write(InformationRecord information)
        {
            if (information == null)
                return;

            _outputAction?.Invoke($"\t{information.MessageData.ToString()} (Tags: {String.Join(",", information.Tags)})");
        }

        protected void Write(DebugRecord debug)
        {
            if (debug == null)
                return;

            _outputAction?.Invoke($"\t{debug.Message}");
        }

        protected void Write(string info)
        {
            if (!String.IsNullOrEmpty(info))
            {
                _outputAction?.Invoke($"\t{info}");
            }
        }

        protected void Write(object item, int indent = 1)
        {
            string indentValue = new String('\t', indent);

            _outputAction?.Invoke($"{indentValue}{item.ToString()} (Type {item.GetType().Name})");

            if (item is string || item is int)
                return;

            if (item is IDynamicMetaObjectProvider)
            {
                _outputAction?.Invoke($"{indentValue}Ja, ein dynamisches Object");
            }
            else if (item is Dictionary<string, object>)
            {
                _outputAction?.Invoke($"{indentValue}Ja, ein dictionary");
            }
            else if (item is PSCustomObject)
            {
                _outputAction?.Invoke($"{indentValue}ja, ein PowerShellObject");
                var psobject = (PSCustomObject)item;

            }
            else
            {

                foreach (var property in item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    if (!property.GetIndexParameters().Any())
                    {
                        object value = property.GetValue(item);

                        if (value is IEnumerable && !(value is string))
                        {
                            _outputAction?.Invoke($"{indentValue}" + $@"{property.Name}");
                            foreach (var enumitem in value as IEnumerable)
                            {
                                _outputAction?.Invoke($"{indentValue}\t" + $@"{enumitem.ToString()}");
                            }
                        }
                        else
                        {
                            _outputAction?.Invoke($"{indentValue}\t" + $@"{property.Name}: {property.GetValue(item)}");
                        }
                    }
                    else
                    {
                        _outputAction?.Invoke($"{indentValue}\t" + $@"Indexproperty: {property.Name}:");
                    }
                }
            }
        }

        protected IEnumerable<T> SelectFromResult<T>(IEnumerable<PSObject> items)
        {
            return items.Where(p => p.BaseObject.GetType() == typeof(T)).Select(p => (T)p.BaseObject);
        }

        /// <summary>
        /// Führt ein PS Skript aus und liefert die dabei aufgetretenen Fehler und 
        /// in die Pipeline geschriebene Objekte.
        /// </summary>
        protected ExecutionResult RunScript(string script, Func<string, string> promptForValueFunc = null, Dictionary<string, object> variables = null, Func<string,string, Collection<ChoiceDescription>, int, int> promptForChoice = null)
        {
            HostCommunicationAdapter communicationAdapter = new HostCommunicationAdapter()
            {
                OnPromptForValue = promptForValueFunc,
                OnPromptForChoice = promptForChoice
            };

            var executer = new ScriptExecuter
            {
                CommunicationAdapter = communicationAdapter,
                Variables = variables
            };

            if (_assemblyToTest != null)
            {
                executer.Assemblies = new List<Assembly>(new[] {_assemblyToTest});
            }



            return executer.Execute(script);
        }


        /// <summary>
        /// Führt ein PS Skript aus und liefert die dabei aufgetretenen Fehler und 
        /// in die Pipeline geschriebene Objekte.
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        protected ExecutionResult RunScriptSecured(string script)
        {
            PermissionSet permissions = new PermissionSet(PermissionState.Unrestricted);

            AppDomain securedDomain = AppDomain.CreateDomain("script execution", null,
                new AppDomainSetup
                {
                    ApplicationName = "script execute",
                    ApplicationBase = Path.GetDirectoryName(new Uri(this.GetType().Assembly.CodeBase).LocalPath),

                },
                permissions);

            var assemblyname = typeof(ScriptExecuter).Assembly.FullName;

            ScriptExecuter executionproxy = securedDomain.CreateInstanceAndUnwrap(assemblyname, typeof(ScriptExecuter).FullName) as ScriptExecuter;

            ExecutionResult result = executionproxy.Execute(script);

            AppDomain.Unload(securedDomain);

            return result;
        }

        protected virtual void OnTestShutDown()
        {

        }

        public virtual void OnTestStart()
        {

        }
    }
}
