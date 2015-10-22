using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Validators
{
    public partial class PropertyValidator
    {
        protected void AssertRegularExpressions(string friendlyName, object value, string pattern, System.Text.RegularExpressions.RegexOptions options)
        {
            throw new Exceptions.NotMatchRegularExpressionPatternException((object)friendlyName);
        }
        protected void AssertLengthBetween(string friendlyName, object value, int minimumLength, int maximumLength)
        {
            long length = (value ?? "").ToString().Length;
            if (length < minimumLength || length > maximumLength)
                throw new Exceptions.LengthNotBetweenMaxAndMinValuesException((object)friendlyName, minimumLength, maximumLength);
        }
        protected void AssertLengthNotLessThan(string friendlyName, object value, int minimumLength)
        {
            long length = (value ?? "").ToString().Length;
            if (length < minimumLength)
                throw new Exceptions.LengthNotLessThanValueException((object)friendlyName, minimumLength);
        }
        protected void AssertLengthNotGreaterThan(string friendlyName, object value, int maximumLength)
        {
            int length = (value ?? "").ToString().Length;
            if (length > maximumLength)
                throw new Exceptions.LengthNotGreaterThanValueException((object)friendlyName, maximumLength);

        }
        protected void AssertIsBetween(string friendlyName, object value, long minimumValue, long maximumValue)
        {
            long number = 0;
            if (value == null || !long.TryParse(value.ToString(), out number) || number < minimumValue || number > maximumValue)
                throw new Exceptions.NotBetweenMaxAndMinValuesException((object)friendlyName, minimumValue, maximumValue);
        }
        protected void AssertNotGreaterThan(string friendlyName, object value, long maximumValue)
        {
            long number = 0;
            if (value == null || !long.TryParse(value.ToString(), out number) || number > maximumValue)
                throw new Exceptions.NotGreaterThanValueException((object)friendlyName, maximumValue);
        }
        protected void AssertNotLessThan(string friendlyName, object value, long minimumValue)
        {
            long number = 0;
            if (value == null || !long.TryParse(value.ToString(), out number) || number < minimumValue)
                throw new Exceptions.NotLessThanValueException((object)friendlyName, minimumValue);
        }
        protected void AssertIsRequired(string friendlyName, Int16 value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, Int32 value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, Int64 value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, Double value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, Single value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, UInt16 value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, UInt32 value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, Guid value)
        {
            if (value == Guid.Empty)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, UInt64 value)
        {
            if (value == 0)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, DateTime value)
        {
            if (value == DateTime.MinValue)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, object value)
        {
            if (value == null)
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
        protected void AssertIsRequired(string friendlyName, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exceptions.IsRequiredException((object)friendlyName);
        }
    }
}
