using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Reflection
{
    public class ParameterInfo : MemberInfoClass
    {
        public Type Type { get; set; }
        public System.Reflection.ParameterInfo Parameter { get; private set; }
        public bool HasDefaultValue { get { return Parameter.HasDefaultValue; } }
        public object DefaultValue { get { return Parameter.DefaultValue; } }
        public bool IsIn { get { return Parameter.IsIn; } }
        public bool IsOut { get { return Parameter.IsOut; } }
        public bool IsOptional { get { return Parameter.IsOptional; } }
        public bool IsRetval { get { return Parameter.IsRetval; } }
        public string Name { get { return Parameter.Name; } }
        public System.Reflection.MemberInfo Member { get { return Parameter.Member; } }
        public bool IsLcid { get { return Parameter.IsLcid; } }
        public int Position { get { return Parameter.Position; } }
        public ParameterInfo(System.Reflection.ParameterInfo parameter)
            : base(parameter.ParameterType)
        {
            Parameter = parameter;
            if (parameter.ParameterType.IsInterface)
            {
                IsGenericType = parameter.ParameterType.UnderlyingSystemType.IsGenericType;
                GenericTypes = new List<Type>();
                if (IsGenericType)
                    GenericTypes = parameter.ParameterType.UnderlyingSystemType.GetGenericArguments();
            }
            else
            {
                IsGenericType = parameter.ParameterType.IsGenericParameter;
                GenericTypes = new List<Type>();
                if (IsGenericType)
                    GenericTypes = parameter.ParameterType.GetGenericArguments();
            }
        }


        
    }
}
