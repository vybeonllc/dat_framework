using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dat.V1.Utils.Validation.Constants
{
    public class RegularExpressionPatterns
    {
        public const string AlphabeticWithOutSpace = "^[a-zA-Z]+$";
        public const string AlphabeticWithSpace = "^[a-zA-Z ]+$";
        public const string AlphaNumeric = "^[a-zA-Z0-9]+$";
        public const string EmailAddress = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        public const string EmailUsername = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]";
        public const string PositiveNumber = @"^\d*\.{0,1}\d+$";
        public const string NegativeNumber = @"^-\d*\.{0,1}\d+$";
        public const string PositiveOrNegativeNumber = @"^-{0,1}\d*\.{0,1}\d+$";
        public const string IntegerNumbers = @"^\d+$";
        public const string PositiveIntegerNumber = @"^\d+$";
        public const string NegativeIntegerNumber = @"^-\d+$";
        public const string BestPassword = @"^.*(?=.{6,})(?=.*[A-Z])(?=.*[\d])(?=.*[\W]).*$";
        public const string StrongPassword = @"a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{6,})(((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\d]))|((?=[a-zA-Z\d\W_]*[A-Z])(?=[a-zA-Z\d\W_]*[\W_]))|((?=[a-zA-Z\d\W_]*[\d])(?=[a-zA-Z\d\W_]*[\W_])))[a-zA-Z\d\W_]*$";
        public const string WeakPassword = @"[a-zA-Z\d\W_]*(?=[a-zA-Z\d\W_]{6,})(?=[a-zA-Z\d\W_]*[A-Z]|[a-zA-Z\d\W_]*[\d]|[a-zA-Z\d\W_]*[\W_])[a-zA-Z\d\W_]*$";
        public const string BadPassword = @"^((^[a-z]{6,}$)|(^[A-Z]{6,}$)|(^[\d]{6,}$)|(^[\W_]{6,}$))$";
        public const string SocialSecurity_Clear = @"^\d{9}$";
        public const string SocialSecurity = @"^\d{3}-\d{2}-\d{4}$";
        public const string ZipCode = @"^\d{5}-\d{4}$";
        public const string ZipCode_Clear = @"^\d{5}$";
        public const string ItemNumber = @"^([A-Za-z]{2})+-+(\d{4}$)";
    }
}
