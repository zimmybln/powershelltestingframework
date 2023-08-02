using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerShellTestingFramework.Tests.Cmdlets
{
    public enum DynamicParameterType
    {
        Unknown = 0,
        RuntimeDefinedDictionary = 1,
        RuntimeDefinedParameter = 2,
    }


    [Cmdlet(VerbsCommon.Watch, "DynamicParameter")]
    public class WatchDynamicParameter : PSCmdlet, IDynamicParameters
    {
        [Parameter(Position = 0, Mandatory = true)]
        public DynamicParameterType DynamicType { get; set; }


        private object? _dynamicParameter = null;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// What does not work
        /// - Returning a single instance of RuntimeDefinedParameter. You should return a RuntimeDefinedParameterDictionary
        /// </remarks>
        /// <returns></returns>
        public object? GetDynamicParameters()
        {
            if (DynamicType == DynamicParameterType.RuntimeDefinedDictionary)
            {
                var collection = new RuntimeDefinedParameterDictionary();

                var parameter = new RuntimeDefinedParameter("Age", typeof(int), new Collection<Attribute>(new[]
                {
                    new ParameterAttribute() { Position = 1, Mandatory = true }
                }));

                collection.Add("Age", parameter);

                _dynamicParameter = collection;
            }
            else if (DynamicType == DynamicParameterType.RuntimeDefinedParameter)
            {
                var parameter = new RuntimeDefinedParameter("Age", typeof(int), new Collection<Attribute>(new[]
                {
                    new ParameterAttribute() { Position = 1, Mandatory = true }
                }));

                _dynamicParameter = parameter;
            }
            
            return _dynamicParameter;
        }

        protected override void ProcessRecord()
        {
            WriteInformation($"Dynamic type {_dynamicParameter?.GetType().Name}", null);
        }


    }
}
