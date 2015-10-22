using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Validators
{
    public partial class PropertyValidator
    {
        protected bool ValidateByAttribute(object obj, System.Reflection.PropertyInfo property, Enumerations.Action action, ref Exceptions.ValidationException exception)
        {
            exception = null;
            object[] validationAttributes = property.GetCustomAttributes(typeof(Attributes.ValidationAttribute), true);
            foreach (Validation.Attributes.ValidationAttribute attr in validationAttributes)
            {
                if (attr.Action != Enumerations.Action.All && attr.Action != action )
                    continue;
                Type type = attr.GetType();
                if (type == typeof(Validation.Attributes.IsBetweenAttribute))
                {
                    Validation.Attributes.IsBetweenAttribute attribute = (Validation.Attributes.IsBetweenAttribute)attr;
                    try
                    {
                        AssertIsBetween(attribute.FriendlyName, property.GetValue(obj), attribute.MinimumValue, attribute.MaximumValue);
                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
                else if (type == typeof(Validation.Attributes.RequiredAttribute) || type == typeof(Validation.Attributes.NotNullAttribute))
                {
                    Validation.Attributes.ValidationAttribute attribute = (Validation.Attributes.ValidationAttribute)attr;
                    try
                    {
                        Type propertyType = property.PropertyType;
                        if (propertyType == typeof(string))
                            AssertIsRequired(attribute.FriendlyName, property.GetValue(obj).ToString());
                        else if (propertyType == typeof(DateTime))
                            AssertIsRequired(attribute.FriendlyName, (DateTime)property.GetValue(obj));
                        else if (propertyType == typeof(Int16))
                            AssertIsRequired(attribute.FriendlyName, (Int16)property.GetValue(obj));
                        else if (propertyType == typeof(Int32))
                            AssertIsRequired(attribute.FriendlyName, (Int32)property.GetValue(obj));
                        else if (propertyType == typeof(Int64))
                            AssertIsRequired(attribute.FriendlyName, (Int64)property.GetValue(obj));
                        else if (propertyType == typeof(Int16))
                            AssertIsRequired(attribute.FriendlyName, (Int16)property.GetValue(obj));
                        else if (propertyType == typeof(Decimal))
                            AssertIsRequired(attribute.FriendlyName, (Decimal)property.GetValue(obj));
                        else if (propertyType == typeof(Single))
                            AssertIsRequired(attribute.FriendlyName, (Single)property.GetValue(obj));
                        else if (propertyType == typeof(UInt16))
                            AssertIsRequired(attribute.FriendlyName, (UInt16)property.GetValue(obj));
                        else if (propertyType == typeof(UInt32))
                            AssertIsRequired(attribute.FriendlyName, (UInt32)property.GetValue(obj));
                        else if (propertyType == typeof(UInt64))
                            AssertIsRequired(attribute.FriendlyName, (UInt64)property.GetValue(obj));
                        else if (propertyType == typeof(Guid))
                            AssertIsRequired(attribute.FriendlyName, (Guid)property.GetValue(obj));
                        else
                            AssertIsRequired(attribute.FriendlyName, property.GetValue(obj));
                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
                else if (type == typeof(Validation.Attributes.LengthNotGreaterThanAttribute))
                {
                    Validation.Attributes.LengthNotGreaterThanAttribute attribute = (Validation.Attributes.LengthNotGreaterThanAttribute)attr;
                    try
                    {
                        AssertLengthNotGreaterThan(attribute.FriendlyName, property.GetValue(obj), attribute.MaximumLength);
                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
                else if (type == typeof(Validation.Attributes.LengthNotLessThanAttribute))
                {
                    Validation.Attributes.LengthNotLessThanAttribute attribute = (Validation.Attributes.LengthNotLessThanAttribute)attr;
                    try
                    {
                        AssertLengthNotLessThan(attribute.FriendlyName, property.GetValue(obj), attribute.MinimumLength);

                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
                else if (type == typeof(Validation.Attributes.NotGreaterThanAttribute))
                {
                    Validation.Attributes.NotGreaterThanAttribute attribute = (Validation.Attributes.NotGreaterThanAttribute)attr;
                    try
                    {
                        AssertNotGreaterThan(attribute.FriendlyName, property.GetValue(obj), attribute.MaximumValue);
                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
                else if (type == typeof(Validation.Attributes.NotLessThanAttribute))
                {
                    Validation.Attributes.NotLessThanAttribute attribute = (Validation.Attributes.NotLessThanAttribute)attr;
                    try
                    {
                        AssertNotLessThan(attribute.FriendlyName, property.GetValue(obj), attribute.MinimumValue);
                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
                else if (type == typeof(Validation.Attributes.RegularExpressionAttribute))
                {
                    Validation.Attributes.RegularExpressionAttribute attribute = (Validation.Attributes.RegularExpressionAttribute)attr;
                    try
                    {
                        AssertRegularExpressions(attribute.FriendlyName, property.GetValue(obj), attribute.Pattern, attribute.Options);
                    }
                    catch (Exceptions.ValidationException ex)
                    {
                        exception = ex;
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
