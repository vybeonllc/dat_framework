using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Validators
{
    [Dat.V1.Utils.Documentation.Attributes.MemberInfo(FullName = "Validator", Summary = "Provide method to validate a class.")]
    public class Validator : PropertyValidator
    {
        public Validator(object obj, Enumerations.Action action = Enumerations.Action.All, string[] Except = null)
        {
            Validate(obj, action, Except);
        }
        public void Validate(object obj, Enumerations.Action action = Enumerations.Action.All, string[] Except = null)
        {
            if (Except == null) Except = new string[] { };
            foreach (System.Reflection.PropertyInfo property in obj.GetType().GetProperties())
            {
                Exceptions.ValidationException exception = null;
                if (Except.Any(e => e == property.Name))
                    continue;
                if (!ValidateByAttribute(obj, property, action, ref exception))
                    throw exception;
                if (!property.PropertyType.IsValueType && (!property.PropertyType.ContainsGenericParameters || (property.PropertyType.ContainsGenericParameters && !property.PropertyType.GenericTypeArguments.GetValue(0).GetType().IsValueType)) && property.PropertyType != typeof(string))
                    Validate(property.GetValue(obj), action);
            }
        }
    }
}
