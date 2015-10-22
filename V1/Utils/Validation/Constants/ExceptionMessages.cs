using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Constants
{
    public class ExceptionMessages
    {
        public const string NotBetweenMaxAndMinValuesException = "{0} was not between {1} and {2}";
        public const string NotNullException = "{0} cannot be null.";
        public const string NotLessThanValueException = "{0} cannot be less than {1}.";
        public const string NotGreaterThanValueException = "{0} cannot be greater than {1}.";
        public const string NotMatchRegularExpressionPatternException = "{0} is not valid.";
        public const string IsRequiredException = "{0} is required.";
        public const string LengthNotBetweenMaxAndMinValuesException = "Length of {0} is not between {1} and {2}.";
        public const string LengthNotGreaterThanValueException = "Length of {0} cannot be more than {1}.";
        public const string LengthNotLessThanValueException = "Length of {0} cannot be less than {1}.";
    
    }
}
