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
        DefinedClass = 3,
        DefinedClassWithParameters = 4,
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

                collection.Add("Age", new RuntimeDefinedParameter("Age", typeof(int), new Collection<Attribute>(new[]
                {
                    new ParameterAttribute() { Position = 1, Mandatory = true }
                })));

                collection.Add("Year", new RuntimeDefinedParameter("Year", typeof(int), new Collection<Attribute>( new[]
                {
                    new ParameterAttribute() { Position = 2, Mandatory = true}
                })));

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
            else if (DynamicType == DynamicParameterType.DefinedClass)
            {
                WriteDebug($"Dynamic parameter {nameof(AgeDynamicParameter)} added");

                _dynamicParameter = new AgeDynamicParameter();
            }
            else if (DynamicType == DynamicParameterType.DefinedClassWithParameters)
            {
                _dynamicParameter = new AgeAndYearDynamicParameter();
            }
            
            return _dynamicParameter;
        }

        protected override void ProcessRecord()
        {
            int age = 0;
            int year = 0;

            if (_dynamicParameter is AgeDynamicParameter ageDynamicParameter)
            {
                age = ageDynamicParameter.Age;
            }
            else if (_dynamicParameter is RuntimeDefinedParameterDictionary runtimeDefinedParameterDictionary)
            {
                age = (int)runtimeDefinedParameterDictionary["Age"].Value;
                year = (int)runtimeDefinedParameterDictionary["Year"].Value;
            }
            else if (_dynamicParameter is AgeAndYearDynamicParameter ageAndYearDynamicParameter)
            {
                age = (int)ageAndYearDynamicParameter.Age;
                year = (int)ageAndYearDynamicParameter.Year;
            }
            
            WriteInformation($"Dynamic type {_dynamicParameter?.GetType().Name} and values Age: {age}, Year: {year}", null);
        }


    }

    public class AgeDynamicParameter
    {
        [Parameter(Mandatory = true, Position = 1)]
        public int Age { get; set; }
    }

    public class AgeAndYearDynamicParameter
    {
        [Parameter(Mandatory = true, Position = 1)]
        public int Age { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public int Year { get; set; }

    }

}
